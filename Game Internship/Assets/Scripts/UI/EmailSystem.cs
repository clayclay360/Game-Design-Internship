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
                case "CRAAP":
                    return "The article is current, relevent, authoritative, accurate, and has purpose. By these measures, it is reliable.";

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
                correctionEmail = GenerateCorrectionEmail("DO NOT VOTE FOR C. ROOK!…", false, "currency", "October 30th, 2013");
                break;
            case 4:
                correctionEmail = GenerateCorrectionEmail("Cameron Rook", true, "currency", "July 5th, 2020");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("C. Rook does WHAAAAAAT?!", false, "curency", "August 27th, 1999");
                break;
            //Part 2
            case 6:
                correctionEmail = GenerateCorrectionEmail("\"I used to babysit… \"", false, "relevancy", "it does not mention Cameron Rook, just a boy named Cameron");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("Investigation of Cameron Rook's involvement in the increase of Crime throughout Canton", true, "relevancy", "it has to do with C. Rook's legislation");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("C. Rook raises taxes, and forgives his own", true, "relevancy", "it discusses C. Rook's tax bill");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("\"Did you guys see the news… \"", false, "currency", "September 17th, 2009");
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
                correctionEmail = GenerateCorrectionEmail("BREAKING: State Attorneys confident in Police Sergeant’s innocence", false, "authority", "are politically motivated");
                break;
            //Part 2
            case 4:
                correctionEmail = GenerateCorrectionEmail("Canton Police Force", false, "relevancy", "it does not involve Kevin Iller");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("\"I have been inundated with questions…\"", true, "authority", "are a detective with a firsthand statement");
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
                correctionEmail = GenerateCorrectionEmail("\"I would never have believed…\"", true, "authority", "directly quote a statement made by the chief of police");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("New evidence links suspect to Canton Clubber killings", false, "currency", "December 22nd, 2002");
                break;
        }
        return correctionEmail;
    }

    public Email DayThreeCorrections(int index)
    {
        switch(index)
        {
            case 1:
                correctionEmail = GenerateCorrectionEmail("Water quality has never been better!", true, "accuracy", "was written by many experts");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("\"My whole family is freaking out…\"", false, "accuracy", "is not backed up by any real sources");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("Canton", true, "accuracy", "comes from a moderated information wiki");
                break;
            //Part 2
            case 4:
                correctionEmail = GenerateCorrectionEmail("Water quality has never been better!", false, "currency", "January 20th, 1980");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("Acid rain: Who to blame?", false, "authority", "are politically motivated");
                break;
            case 6:
                correctionEmail = GenerateCorrectionEmail("Today’s Acid Rain", true, "accuracy", "has a source backing it up");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("\"Everybody is complaining about their cars…\"", false, "relevancy", "brings up an issue unrelated to the acid rain");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("OMG, this acid rain has got to stop!", false, "accuracy", "does not confirm whether the problem was caused by the acid rain");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("Regulations of factories repealed, government forgets to inform the public.", true, "accuracy", "provides factual information");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("\"I can't believe people haven't realized it…\"", false, "accuracy", "does not provide any evidence for its statements");
                break;
        }
        return correctionEmail;
    }

    public Email DayFourCorrections(int index)
    {
        switch (index)
        {
            case 1:
                correctionEmail = GenerateCorrectionEmail("Canton", true, "purpose", "give information about the Spanish Flu pandemic");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("Revisiting our history", false, "purpose", "make people think Canton is the greatest nation in the world");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("Canton’s Grand Mayors and their Contributions", false, "purpose", "make Canton's mayors look good");
                break;
            case 4:
                correctionEmail = GenerateCorrectionEmail("Canton’s industrial revolution", true, "purpose", "provide data on Canton's economy");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("Where did Canton come from?", false, "purpose", "glorify religion");
                break;
            //Part 2
            case 6:
                correctionEmail = GenerateCorrectionEmail("Centon is Awesome!", false, "relevancy", "does not provide any important information");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("Sociology of Canton: History of Ethics in the Workplace", false, "currency", "December 4th, 1964");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("Remembering the Downtown Inferno Crisis", true, "purpose", "provide information about a historical event");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("\" You know what, I’m gonna say it.\"", false, "accuracy", "does not provide any sources");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("Canton’s History of Conservation", true, "purpose", "provide information about Canton's efforts in nature conservation");
                break;
        }
        return correctionEmail;
    }

    public Email DayFiveCorrections(int index)
    {
        switch (index)
        {
            case 1:
                correctionEmail = GenerateCorrectionEmail("I took all of my parent's money!", false, "relevancy", 
                    "despite having to do with the outcome of the hack, it gives no information about the attack itself");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("All eyes on you: How the government accesses your information", true, "CRAAP", "");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("Department of Information remarks on security of Canton’s data vaults", true, "CRAAP", "");
                break;
            case 4:
                correctionEmail = GenerateCorrectionEmail("Cash Catastrophe!", false, "accuracy", "is based on an opinion and not backed up with sources");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("\"I can't believe it!\"", false, "currency", "September 10th, 2001");
                break;
            case 6:
                correctionEmail = GenerateCorrectionEmail("\"All these beta males…\"", false, "purpose", "sell the influencer's program");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("PCs for the Public Program", true, "CRAAP", "");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("\"People of Canton…\"", true, "CRAAP", "");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("Code Cobras responsible for Data breach, Data vaults unaffected.", false, "purpose", 
                    "shield the government from any blame for the incident");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("\"All you worshippers…\"", false, "relevancy", "where Old man Cletus hides his money has nothing to do with this data breach");
                break;
        }
        return correctionEmail;
    }

    public Email DaySixCorrections(int index)
    {
        switch (index)
        {
            case 1:
                correctionEmail = GenerateCorrectionEmail("The Cash Act", true, "CRAAP", "");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("\"Soon the Grand Mayor…\"", false, "relevancy", "it is advertising a giveaway");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("Concerns about the Cash Act", true, "CRAAP", "");
                break;
            case 4:
                correctionEmail = GenerateCorrectionEmail("\"I don't get what the hubbub…\"", false, "accuracy", "it does not accurately describe the consequences of the bill");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("Government owned businesses: How do they affect us?", false, "currency", "March 12th, 1984");
                break;
            case 6:
                correctionEmail = GenerateCorrectionEmail("What’s up with this Cash Act?", false, "authority", "are pursuing their own selfish goals");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("Finances and Fascism: Canton’s Economy Projections", true, "CRAAP", "");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("Canton’s Cash Act redistributes wealth at the sacrifice of many rights.", true, "CRAAP", "");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("The Cash Act to save the economy!", false, "purpose", "glorify the government");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("\"WE NEED MORE GOVERNMENT!\"", false, "purpose", "advocate for the government");
                break;
        }
        return correctionEmail;
    }

    public Email DaySevenCorrections(int index)
    {
        switch (index)
        {
            case 1:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 2:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 3:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 4:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 5:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 6:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 7:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 8:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 9:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
                break;
            case 10:
                correctionEmail = GenerateCorrectionEmail("", true, "", "");
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
