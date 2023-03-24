using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//TODO: Add From/Subject to base email so it's not in every line
public class EmailSystem : MonoBehaviour
{
    public Email currentEmail;
    [Header("UI")]
    public GameObject emailPanel;
    public GameObject inboxPanel;
    public GameObject notifcations;
    public Text buttonText;
    public Text notificationText;
    public Button toggleBtn;
    public Email[] Inbox;
    public Scrollbar scrollBar;

    [Header("Inbox")]
    public Button[] InboxBtns;
    public Text[] InboxSubjects;
    public Text[] InboxFroms;

    [Header("Email Parts")]
    public Text subject;
    public Text sender;
    public Text body;

    private int currentEmailIndex;
    private int numberOfUnreadEmails;
    private bool emailUpToDate;

    public Email correctionEmail;

    public void Start()
    {
        DisplayEmails();
        correctionEmail = gameObject.AddComponent<Email>();
    }

    public void DisplayEmails()
    {
        SortInbox();
        for (int i = 0; i < Inbox.Length; i++)
        {
            if (Inbox[i] != null)
            {
                InboxSubjects[i].text = Inbox[i].subject;
                InboxFroms[i].text = Inbox[i].sender;
                InboxBtns[i].gameObject.SetActive(true);
            }
            else
            {
                InboxBtns[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeEmail(Email email)
    {
        subject.text = email.subject;
        sender.text = email.sender;
        body.text = email.body;
    }

    public void AddEmail(Email email)
    {
        SortInbox();
        Inbox[FindLowestEmpty()] = email;
    }

    #region Correction Emails
    /// <summary>
    /// Uses its parameters to generate an email telling the player that they got a question wrong.
    /// </summary>
    /// <returns></returns>
    public Email GenerateCorrectionEmail(string articleTitle, bool isReliable, string pieceOfCRAAP, string correctionInfo)
    {
        string reliableString = isReliable ? "unreliable, when it is actually reliable" : 
                                             "reliable, when it is actually unreliable";

        correctionEmail.subject = $"Correction about \"{articleTitle}\"";
        correctionEmail.sender = "Boss"; //Change this later? Add story?
        correctionEmail.body = "Dear *,\n\nIt appears you have made an error " +
                               $"in regards to a recent article you reviewed titled {articleTitle}. " +
                               $"The article was marked incorrectly as {reliableString}.\n\n" +
                               $"The reliability of the information has to do with its {pieceOfCRAAP}. " +
                               $"{sortCRAAP(isReliable, pieceOfCRAAP, correctionInfo)}" +
                               $" Please have more diligence, and check the database if you are having trouble.";

        return correctionEmail;
    }

    private string sortCRAAP(bool isReliable, string pieceOfCRAAP, string correctionInfo)
    {
        if (isReliable)
        {
            switch (pieceOfCRAAP)
            {
                case "currency":
                    return $"Information less than fifteen years old is considered reliable, and the article was published on {correctionInfo}.";
                case "relevancy":
                    return $"The information is actually relevant to the topic you are researching, because {correctionInfo}.";
                case "authority":
                    return $"The author of the work is trustworthy, because they {correctionInfo}.";
                case "accuracy":
                    return $"The information in the article is most likely accurate, because it {correctionInfo}.";
                case "purpose":
                    return $"The article's information can be considered reliable, because its purpose is to {correctionInfo}.";
            }
        }
        else
        {
            switch (pieceOfCRAAP)
            {
                case "currency":
                    return $"Information older than fifteen years old is considered unreliable, and the article was published on {correctionInfo}.";
                case "relevancy":
                    return $"The information is not relevant to the topic you are researching, because {correctionInfo}.";
                case "authority":
                    return $"The author of the work is not trustworhy, because they {correctionInfo}.";
                case "accuracy":
                    return $"The information in the article may not be accurate, because it {correctionInfo}.";
                case "purpose":
                    return $"The article's information might not be accurate, because its purpose is to {correctionInfo}.";
            }
        }
        Debug.LogWarning("Your pieceOfCRAAP fell through the switch statements in the sortCRAAP method!" +
                         "\nDouble check that your spelled it correctly and it's all lowercase!");
        return "WHOA!! This shouldn't be here!";
    }
    
    public Email DayOneCorrections(int index)
    {
        switch (index)
        {
            case 1: //Counilman Cameron Rook steals candy from a baby //RELIABLE
                correctionEmail = GenerateCorrectionEmail("Councilman Cameron Rook steals candy from baby", true, "currency", "June 5th, 2019");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("C. Rook is a monster!", false, "currency", "January 6th, 2001");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("DO NOT VOTE FOR C. ROOK!�", false, "currency", "October 30th, 2013");
                break;
            case 4:
                correctionEmail = GenerateCorrectionEmail("Cameron Rook", true, "currency", "July 5th, 2020");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("C. Rook does WHAAAAAAT?!", false, "curency", "August 27th, 1999");
                break;
            //Part 2
            case 6:
                correctionEmail = GenerateCorrectionEmail("\"I used to babysit� \"", false, "relevancy", "it does not mention Cameron Rook, just a boy named Cameron");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("Investigation of Cameron Rook's involvement in the increase of Crime throughout Canton", true, "relevancy", "it has to do with C. Rook's legislation");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("C. Rook raises taxes, and forgives his own", true, "relevancy", "it discusses C. Rook's tax bill");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("\"Did you guys see the news� \"", false, "currency", "September 17th, 2009");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("Monsters Among Us", false, "relevancy", "it doesn't have to do with C. Rook's policies");
                break;
        }
        return correctionEmail;
    }

    public Email DayTwoCorrections(int index)
    {
        switch (index)
        {
            case 1:
                correctionEmail = GenerateCorrectionEmail("Canton's Police Force, Void of Accountability", true, "authority", "published it in an academic journal");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("\"They've got the wrong guy!\"", false, "authority", "have no credentials or sources");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("BREAKING: State Attorneys confident in Police Sergeant�s innocence", false, "authority", "are a government-sanctioned journalist");
                break;
            //Part 2
            case 4:
                correctionEmail = GenerateCorrectionEmail("Canton Police Force", false, "relevancy", "it does not involve Kevin Iller");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("\"I have been inundated with questions�\"", true, "authority", "are a detective with a firsthand statement");
                break;
            case 6:
                correctionEmail = GenerateCorrectionEmail("We did it Saiddit!", false, "authority", "did not back up their statement with any credibility");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("Canton Clubber sleeping with Grand Mayor?!", false, "relevancy", "it does not have to do with the murder case");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("Marissa Urder", true, "authority", "are an archive of official documents");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("\"I would never have believed�\"", true, "authority", "directly quote a statement made by the chief of police");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("New evidence links suspect to Canton Clubber killings", false, "currency", "December 22nd, 2002");
                break;
        }
        
        return correctionEmail;
    }

    #endregion

    public void SortInbox()
    {
        if (FindLowestEmpty() == 99)
        {
            return;
        }
        //Find the number of emails we have
        int emails = 0;
        for (int i = 0; i < Inbox.Length; i++)
        {
            if (Inbox[i])
            {
                emails += 1;
            }
        }

        //For each email see if we can sort it
        for (int e = 0; e < emails; e++)
        {
            for (int i = FindLowestEmpty() + 1; i < Inbox.Length; i++)
            {
                if (Inbox[i])
                {
                    Inbox[FindLowestEmpty()] = Inbox[i];
                    Inbox[i] = null;
                }
            }
        }

    }

    public int FindLowestEmpty()
    {
        for (int i = 0; i < Inbox.Length; i++)
        {
            if (!Inbox[i])
            {
                return i;
            }
        }
        return 99; //Inbox full
    }


    /// <summary>
    /// Set buttons to NONE in navigation sto stop them from continuously calling their methods
    /// </summary>
    public void ShowHide()
    {
        DisplayEmails();
        inboxPanel.SetActive(!inboxPanel.activeSelf); //switch the active state
        buttonText.text = inboxPanel.activeSelf ? "Hide Inbox" : "Show Inbox";
    }

    public void ShowEmail(int btnNum) //Accessed from button
    {
        ChangeEmail(Inbox[btnNum]);
        emailPanel.gameObject.SetActive(true);
        currentEmailIndex = btnNum;
        scrollBar.value = 1;
    }

    public void HideEmail() //Accessed from button
    {
        emailPanel.gameObject.SetActive(false);
        Inbox[currentEmailIndex].opened = true;
    }

    public void DeleteEmail()
    {
        Inbox[currentEmailIndex].opened = true;
        emailPanel.SetActive(false);
        Inbox[currentEmailIndex] = null;
        DisplayEmails();
        if (Inbox[0] == null)
        {
            ShowHide();
        }
    }

    private void Update()
    {
        CheckEmails();
        GameManager.readyToStartWork = emailUpToDate; 
    }

    public void CheckEmails() 
    {
        bool allEmailsDeleted = true;

        // check if all emails are read
        for (int i = 0; i < Inbox.Length; i++)
        {
            if (Inbox[i] != null)
            {
                allEmailsDeleted = false;

                if (!Inbox[i].opened)
                {
                    emailUpToDate = false;
                    break;
                }
                emailUpToDate = true;
            }
        }
        
        //check if all emails are deleted
        if (allEmailsDeleted)
        {
            emailUpToDate = true;
        }

        numberOfUnreadEmails = 0;

        //check how many emails aren;t read
        #region Notifications
        for (int c = 0; c < Inbox.Length; c++)
        {
            if (Inbox[c] != null) 
            {
                if (!Inbox[c].opened)
                {
                    numberOfUnreadEmails++;
                }
            }
        }
        if(numberOfUnreadEmails == 0)
        {
            notifcations.SetActive(false);
        }
        else
        {
            notifcations.SetActive(true);
        }
        notificationText.text = numberOfUnreadEmails.ToString();
        #endregion
    }

}
