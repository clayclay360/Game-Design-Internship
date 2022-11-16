using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup blackScreen;
    public Text questionText;
    public Text dayText;
    public GameObject informationContainer;
    public GameObject answerPrompt;
    public EndingNewspaper newsPaper;

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

        GameManager.currentDay = 0;
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
        dayText.text = "";
        blackScreen.blocksRaycasts = false;
        #endregion

        #region ReadEmails
        while (!GameManager.readyToStartWork)
        {
            yield return null;
        }
        #endregion
        
        yield return new WaitForSeconds(1);
        questionText.gameObject.SetActive(true);
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

            yield return new WaitForSeconds(.5f);
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

        #region Relevancy
        // start passing out information
        yield return new WaitForSeconds(1);

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

            yield return new WaitForSeconds(.5f);
        }
        #endregion

        answerPrompt.SetActive(true);
        Debug.Log("End Task");
    }

    // create the source and fill in the information
    public void CreateSource(Source.type sourceType)
    {
        GameObject informationPrefab = null;

        switch (sourceType)
        {
            case Source.type.article:
                informationPrefab = Instantiate(informationType.articlePrefab, informationContainer.transform);
                break;
            case Source.type.social:
                informationPrefab = Instantiate(informationType.socialsPrefab, informationContainer.transform);
                break;
            case Source.type.research:
                break;
            case Source.type.newspaper:
                informationPrefab = Instantiate(informationType.newspaperPrefab, informationContainer.transform);
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
        //TODO: This has to be changed to be affected by the choice the player makes
        if (GameManager.correctlySorted >= GameManager.correctNeeded[GameManager.currentDay])
        {
            //Populate newspaper goodresult
            //Will do something fancier eventually
            string headline = "NEW POLITICAL SCANDAL UNVEILED";
            string deck = "CANDIDATE IS CORRUPT";
            string article = "Investigations have revealed new details surrounding one of the local mayoral candidates and several dubious connections. The candidate has dropped out of the race amid these allegations but has made no comment as to the truthfulness of the accusations against them.";
            newsPaper.SetNewspaper(headline, deck, article);
            Debug.Log("Good Ending");
        }
        else
        {
            //Populate newspaper badresult
            string headline = "ELECTION IN CLOSE RACE";
            string deck = "CANDIDATE B LEADING POLLS";
            string article = "Candidate B's lead over Candidate A remains even amidst allegations of corruption. Candidate B is choosing not to let rumors affect their campaign, saying that no solid evidence against them has been produced.";
            newsPaper.SetNewspaper(headline, deck, article);
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
            Debug.Log("Continue");
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator newsPaperAnim()
    {
        newsPaper.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        newsPaper.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        
        //Transition
        #region Display
        blackScreen.blocksRaycasts = true;
        yield return new WaitForSeconds(.25f);

        while (blackScreen.alpha != 1)
        {
            blackScreen.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        #endregion
        //Play the newspaper animation and wait for the player to click
        yield return StartCoroutine(newsPaperAnim());
        #region NextDay
        //Reset to start next level
        GameManager.correctlySorted = 0;
        dayText.text = GameManager.dayText[GameManager.currentDay];
        questionText.gameObject.SetActive(false);
        StartCoroutine(Gameloop());
        #endregion
    }

}
