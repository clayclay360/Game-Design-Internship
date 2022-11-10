using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup blackScreen;
    public Text questionText;
    public GameObject draggableUI;

    [Header("Questions")]
    public Question[] questions;

    [Header("Emails")]
    public Email[] emails;

    private int sourceIndex;

    private InformationType informationType;
    private EmailSystem emailSystem;

    private void Start()
    {
        informationType = GetComponent<InformationType>();
        emailSystem = FindObjectOfType<EmailSystem>();

        StartCoroutine(Gameloop());
    }

    public IEnumerator Gameloop()
    {
        // start the day and display question
        #region Display
        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        while(blackScreen.alpha != 0)
        {
            blackScreen.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        blackScreen.blocksRaycasts = false;
        #endregion

        #region ReadEmails
        while (!GameManager.readyToStartWork)
        {
            yield return null;
        }
        #endregion
        
        yield return new WaitForSeconds(1);
        questionText.text = questions[GameManager.currentDay].question;

        #region Currency
        // start passing out information
        yield return null;
        int numberOfReviewedInformation = 0;

        while(numberOfReviewedInformation < 2/*questions[GameManager.currentDay].sources.Length*/)
        {
            //temporary
            sourceIndex = numberOfReviewedInformation;
            CreateSource(questions[GameManager.currentDay].sources[sourceIndex].Type);
            numberOfReviewedInformation++;
            
            //wait until player finishes sources
            while (!GameManager.readyForNextSource)
            {
                yield return null;
            }
            GameManager.readyForNextSource = false;

            yield return new WaitForSeconds(2);
        }
        #endregion

        //count the emails
        int currentEmailIndex = 0;

        for(int i = 0; i < emailSystem.Inbox.Length; i++)
        {
            if(emailSystem.Inbox[i] == null && currentEmailIndex < emails.Length)
            {
                emailSystem.Inbox[i] = emails[currentEmailIndex];
                currentEmailIndex++;
            }
        }

        #region ReadEmails
        GameManager.readyToStartWork = false;
        while (!GameManager.readyToStartWork)
        {
            yield return null;
        }
        #endregion

        #region Currency
        // start passing out information
        yield return null;

        while (numberOfReviewedInformation < 4/*questions[GameManager.currentDay].sources.Length*/)
        {
            //temporary
            sourceIndex = numberOfReviewedInformation;
            CreateSource(questions[GameManager.currentDay].sources[sourceIndex].Type);
            numberOfReviewedInformation++;

            //wait until player finishes sources
            while (!GameManager.readyForNextSource)
            {
                yield return null;
            }
            GameManager.readyForNextSource = false;

            yield return new WaitForSeconds(2);
        }
        #endregion

        Debug.Log("End Task");
    }

    // create the source and fill in the information
    public void CreateSource(Source.type sourceType)
    {
        GameObject informationPrefab = null;

        switch (sourceType)
        {
            case Source.type.article:
                informationPrefab = Instantiate(informationType.articlePrefab, draggableUI.transform);
                break;
            case Source.type.social:
                informationPrefab = Instantiate(informationType.socialsPrefab, draggableUI.transform);
                break;
            case Source.type.research:
                break;
            case Source.type.newspaper:
                informationPrefab = Instantiate(informationType.newspaperPrefab, draggableUI.transform);
                break;
        }

        Information information = informationPrefab.GetComponent<Information>();
        information.sourceText.text = questions[GameManager.currentDay].sources[sourceIndex].source;
        information.titleText.text = questions[GameManager.currentDay].sources[sourceIndex].title;
        information.authorText.text = questions[GameManager.currentDay].sources[sourceIndex].author;
        information.informationText.text = questions[GameManager.currentDay].sources[sourceIndex].information;
        information.monthText.text = questions[GameManager.currentDay].sources[sourceIndex].month;
        information.yearText.text = questions[GameManager.currentDay].sources[sourceIndex].year.ToString();
        information.isReliable = questions[GameManager.currentDay].sources[sourceIndex].isReliable;
    }

}
