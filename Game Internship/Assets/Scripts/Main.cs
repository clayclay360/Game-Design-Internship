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
    public Text LeftText;
    public Text RightText;
    public GameObject informationContainer;
    public GameObject answerPrompt;
    public EndingNewspaper newsPaper;
    private Articles articles;

    [Header("Questions")]
    public Question[] questions;

    private int sourceIndex;
    private int numberOfSourcesInDay;

    private InformationType informationType;
    private EmailSystem emailSystem;

    private void Start()
    {
        informationType = GetComponent<InformationType>();
        emailSystem = FindObjectOfType<EmailSystem>();
        articles = new Articles();
        newsPaper.gameObject.SetActive(false);

        GameManager.currentDay = 0; // for testing
        StartCoroutine(Gameloop());
    }

    public IEnumerator Gameloop()
    {
        GameManager.numberOfCorrectSources = 0;

        if (GameManager.currentDay == 0)
        {
            numberOfSourcesInDay = questions[GameManager.currentDay].sources.Length;

            // start the day and display question
            #region Display

            blackScreen.gameObject.SetActive(true);
            blackScreen.alpha = 1;
            yield return new WaitForSeconds(2);

            while (blackScreen.alpha != 0)
            {
                blackScreen.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            dayText.text = "";
            blackScreen.blocksRaycasts = false;
            #endregion

            #region ReadEmails
            for (int i = 0; i < 1; i++)
            {
                emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[i];
            }
            yield return null;

            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region DisplayQuestion
            //yield return new WaitForSeconds(1);
            //questionText.gameObject.SetActive(true);
            //questionText.text = questions[GameManager.currentDay].question;
            #endregion

            #region Currency
            // start passing out information
            yield return null;
            int numberOfReviewedInformation = 0;

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

            #region ReadEmails

            //count the emails
            int currentEmailIndex = 1;

            for (int i = 0; i < emailSystem.Inbox.Length; i++)
            {
                if (emailSystem.Inbox[i] == null && currentEmailIndex < questions[GameManager.currentDay].emails.Length)
                {
                    //add next wave of emails
                    emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[currentEmailIndex];
                    currentEmailIndex++;
                }
            }


            GameManager.readyToStartWork = false;
            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region Relevancy
            // start passing out information
            yield return new WaitForSeconds(1);

            while (numberOfReviewedInformation < 8/*questions[GameManager.currentDay].sources.Length*/)
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

            #region GetAnswers
            //LeftText.text = questions[GameManager.currentDay].answers[0];
            //RightText.text = questions[GameManager.currentDay].answers[1];
            #endregion

            //answerPrompt.SetActive(true);
            Debug.Log("End Task");
        }
        else if (GameManager.currentDay == 1)
        {
            numberOfSourcesInDay = questions[GameManager.currentDay].sources.Length;

            // start the day and display question
            #region Display
            blackScreen.gameObject.SetActive(true);
            blackScreen.alpha = 1;
            yield return new WaitForSeconds(2);

            while (blackScreen.alpha != 0)
            {
                blackScreen.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            dayText.text = "";
            blackScreen.blocksRaycasts = false;
            #endregion

            #region ReadEmails
            for (int i = 0; i < 1; i++)
            {
                emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[i];
            }
            yield return null;

            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region DisplayQuestion
            //yield return new WaitForSeconds(1);
            //questionText.gameObject.SetActive(true);
            //questionText.text = questions[GameManager.currentDay].question;
            #endregion

            #region Authority
            // start passing out information
            yield return null;
            int numberOfReviewedInformation = 0;

            while (numberOfReviewedInformation < 3)
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

            #region ReadEmails

            //count the emails
            int currentEmailIndex = 1;

            for (int i = 0; i < emailSystem.Inbox.Length; i++)
            {
                if (emailSystem.Inbox[i] == null && currentEmailIndex < questions[GameManager.currentDay].emails.Length)
                {
                    //add next wave of emails
                    Debug.Log("EmailAdd");
                    emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[currentEmailIndex];
                    currentEmailIndex++;
                }
            }


            GameManager.readyToStartWork = false;
            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region MoreAuthority
            // start passing out information
            yield return new WaitForSeconds(1);

            while (numberOfReviewedInformation < 9/*questions[GameManager.currentDay].sources.Length*/)
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

            #region GetAnswers
            //LeftText.text = questions[GameManager.currentDay].answers[0];
            //RightText.text = questions[GameManager.currentDay].answers[1];
            #endregion

            //answerPrompt.SetActive(true);
            Debug.Log("End Task");
        }
        else if( GameManager.currentDay == 2)
        {
            numberOfSourcesInDay = questions[GameManager.currentDay].sources.Length;

            // start the day and display question
            #region Display
            blackScreen.gameObject.SetActive(true);
            blackScreen.alpha = 1;
            yield return new WaitForSeconds(2);

            while (blackScreen.alpha != 0)
            {
                blackScreen.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            dayText.text = "";
            blackScreen.blocksRaycasts = false;
            #endregion

            #region ReadEmails
            for (int i = 0; i < 1; i++)
            {
                emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[i];
            }
            yield return null;

            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region DisplayQuestion
            //yield return new WaitForSeconds(1);
            //questionText.gameObject.SetActive(true);
            //questionText.text = questions[GameManager.currentDay].question;
            #endregion

            #region Accuracy
            // start passing out information
            yield return null;
            int numberOfReviewedInformation = 0;

            while (numberOfReviewedInformation < 3)
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

            #region ReadEmails

            //count the emails
            int currentEmailIndex = 1;

            for (int i = 0; i < emailSystem.Inbox.Length; i++)
            {
                if (emailSystem.Inbox[i] == null && currentEmailIndex < questions[GameManager.currentDay].emails.Length)
                {
                    //add next wave of emails
                    Debug.Log("EmailAdd");
                    emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[currentEmailIndex];
                    currentEmailIndex++;
                }
            }


            GameManager.readyToStartWork = false;
            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region MoreAccuracy
           //start passing out information
           yield return new WaitForSeconds(1);

            while (numberOfReviewedInformation < 10/*questions[GameManager.currentDay].sources.Length*/)
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

            #region GetAnswers
            //LeftText.text = questions[GameManager.currentDay].answers[0];
            //RightText.text = questions[GameManager.currentDay].answers[1];
            #endregion

            //answerPrompt.SetActive(true);
            Debug.Log("End Task");
        }
        else if (GameManager.currentDay == 3)
        {
            numberOfSourcesInDay = questions[GameManager.currentDay].sources.Length;

            // start the day and display question
            #region Display
            blackScreen.gameObject.SetActive(true);
            blackScreen.alpha = 1;
            yield return new WaitForSeconds(2);

            while (blackScreen.alpha != 0)
            {
                blackScreen.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            dayText.text = "";
            blackScreen.blocksRaycasts = false;
            #endregion

            #region ReadEmails
            for (int i = 0; i < 1; i++)
            {
                emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[i];
            }
            yield return null;

            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region DisplayQuestion
            //yield return new WaitForSeconds(1);
            //questionText.gameObject.SetActive(true);
            //questionText.text = questions[GameManager.currentDay].question;
            #endregion

            #region Purpose
            // start passing out information
            yield return null;
            int numberOfReviewedInformation = 0;

            while (numberOfReviewedInformation < 5)
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

            #region ReadEmails

            //count the emails
            int currentEmailIndex = 1;

            for (int i = 0; i < emailSystem.Inbox.Length; i++)
            {
                if (emailSystem.Inbox[i] == null && currentEmailIndex < questions[GameManager.currentDay].emails.Length)
                {
                    //add next wave of emails
                    Debug.Log("EmailAdd");
                    emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[currentEmailIndex];
                    currentEmailIndex++;
                }
            }


            GameManager.readyToStartWork = false;
            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region MorePurpose
            // start passing out information
            yield return new WaitForSeconds(1);

            while (numberOfReviewedInformation < 10/*questions[GameManager.currentDay].sources.Length*/)
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

            #region GetAnswers
            //LeftText.text = questions[GameManager.currentDay].answers[0];
            //RightText.text = questions[GameManager.currentDay].answers[1];
            #endregion

            //answerPrompt.SetActive(true);
            Debug.Log("End Task");
        }
        else if (GameManager.currentDay == 4)
        {
            numberOfSourcesInDay = questions[GameManager.currentDay].sources.Length;

            // start the day and display question
            #region Display
            blackScreen.gameObject.SetActive(true);
            blackScreen.alpha = 1;
            yield return new WaitForSeconds(2);

            while (blackScreen.alpha != 0)
            {
                blackScreen.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            dayText.text = "";
            blackScreen.blocksRaycasts = false;
            #endregion

            #region ReadEmails
            for (int i = 0; i < 1; i++)
            {
                emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[i];
            }
            yield return null;

            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region DisplayQuestion
            //yield return new WaitForSeconds(1);
            //questionText.gameObject.SetActive(true);
            //questionText.text = questions[GameManager.currentDay].question;
            #endregion

            #region Fifth Day
            // start passing out information
            yield return null;
            int numberOfReviewedInformation = 0;

            while (numberOfReviewedInformation < 7)
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

            #region ReadEmails

            //count the emails
            int currentEmailIndex = 1;

            for (int i = 0; i < emailSystem.Inbox.Length; i++)
            {
                if (emailSystem.Inbox[i] == null && currentEmailIndex < questions[GameManager.currentDay].emails.Length)
                {
                    //add next wave of emails
                    Debug.Log("EmailAdd");
                    emailSystem.Inbox[i] = questions[GameManager.currentDay].emails[currentEmailIndex];
                    currentEmailIndex++;
                }
            }


            GameManager.readyToStartWork = false;
            while (!GameManager.readyToStartWork)
            {
                yield return null;
            }
            #endregion

            #region MoreFifthDay
            // start passing out information
            yield return new WaitForSeconds(1);

            while (numberOfReviewedInformation < 10/*questions[GameManager.currentDay].sources.Length*/)
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

            #region GetAnswers
            //LeftText.text = questions[GameManager.currentDay].answers[0];
            //RightText.text = questions[GameManager.currentDay].answers[1];
            #endregion

            //answerPrompt.SetActive(true);
            Debug.Log("End Task");
        }

        EndDay(PlayerPasses());
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
        information.index = questions[GameManager.currentDay].sources[sourceIndex].index;

        if(information.sriteRenderer != null)
        {
            information.sriteRenderer.sprite = questions[GameManager.currentDay].sources[sourceIndex].characterImage;
        }
    }

    public bool PlayerPasses()
    {
        if(numberOfSourcesInDay / 2 < GameManager.numberOfCorrectSources)
        {
            Debug.Log("Player Passes");
            return true;
        }
        Debug.Log("Player Failed");
        return false;
    }

    public void EndDay(bool isPlayerCorrect)
    {
        //Get the newspaper info from the Articles script
        string[] paperInfo = articles.GetNewspaperInfo(GameManager.currentDay, isPlayerCorrect);
        newsPaper.SetNewspaper(paperInfo[0], paperInfo[1], paperInfo[2]);
        GameManager.currentDay += 1; //Increment the Day
        //Check if that was the last level
        if (GameManager.currentDay > GameManager.totalDays) //Subtract one because arrays start at zero!
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
        newsPaper.spinAnimation.Play();
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
