using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup blackScreen;
    public Text questionText, dayText;
    public GameObject draggableUI;

    [Header("Questions")]
    public Question[] questions;

    private int sourceIndex;

    private InformationType informationType;
    private void Start()
    {
        informationType = GetComponent<InformationType>();
        GameManager.correctlySorted = 0;
        StartCoroutine(Gameloop());
    }

    public IEnumerator Gameloop()
    {
        // start the day and display question
        #region Display
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 1;
        yield return new WaitForSeconds(2);

        while(blackScreen.alpha != 0)
        {
            blackScreen.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        blackScreen.blocksRaycasts = false;

        yield return new WaitForSeconds(1);
        questionText.text = questions[GameManager.currentDay].question;
        questionText.gameObject.SetActive(true);
        #endregion

        // start passing out information
        yield return null;
        int numberOfReviewedInformation = 0;
        while(numberOfReviewedInformation < questions[GameManager.currentDay].sources.Length)
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

        EndDay();
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

    public void EndDay()
    {
        if (GameManager.correctlySorted >= GameManager.correctNeeded[GameManager.currentDay])
        {
            Debug.Log("Good Ending");
        }
        else
        {
            Debug.Log("Bad Ending");
        }
        GameManager.currentDay += 1;
        //Check if that was the last level
        if (GameManager.currentDay > GameManager.totalDays + 1) //Add one because arrays start at zero!
        {
            Debug.Log("End Game");
        }
        else
        {
            //Reset to start next level
            GameManager.correctlySorted = 0;
            dayText.text = GameManager.dayText[GameManager.currentDay];
            questionText.gameObject.SetActive(false);
            StartCoroutine(Gameloop());
        }
    }

}
