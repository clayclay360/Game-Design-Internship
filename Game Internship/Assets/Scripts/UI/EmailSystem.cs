using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//This script manages the email system during the gameplay loop
//Note: The email system right now is only able to handle up to 5 emails in the inbox at once.
//There are situations where the player might have more than 5 emails, so this may require some fixing-up.
public class EmailSystem : MonoBehaviour
{
    #region variables
    public Email currentEmail;
    //References to the gameObjects used by the email system
    [Header("UI")]
    public GameObject emailPanel;
    public GameObject inboxPanel;
    public GameObject notifcations;
    public Text notificationText;
    public Button toggleBtn;
    public Email[] Inbox;
    public Scrollbar scrollBar;
    //Arrays for the individual objects in the inbox that the player can select to view emails
    [Header("Inbox")]
    public Button[] InboxBtns;
    public Text[] InboxSubjects;
    public Text[] InboxFroms;
    //Text elements that we update with strings to populate emails
    [Header("Email Parts")]
    public Text subject;
    public Text sender;
    public Text body;
    //Extra for keeping track of the emails
    private int currentEmailIndex;
    private int numberOfUnreadEmails;
    private bool emailUpToDate;
    #endregion

    #region Email Management
    public void Start()
    {
        DisplayEmails();
    }

    /// <summary>
    /// Sets up the inbox elements to view the current emails
    /// </summary>
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
    /// <summary>
    /// Changes the open email text elements to reflect the currently selected email
    /// </summary>
    /// <param name="email"></param>
    public void ChangeEmail(Email email)
    {
        subject.text = email.subject;
        sender.text = email.sender;
        body.text = email.body;
    }
    /// <summary>
    /// Adds an email to the inbox in the lowest empty place.
    /// </summary>
    /// <param name="email">The email we're adding to the inbox</param>
    public void AddEmail(Email email)
    {
        SortInbox();
        Inbox[FindLowestEmpty()] = email;
    }
    #endregion

    /// <summary>
    /// This whole region is for sending the player the emails that tell them when they get an article wrong.
    /// </summary>
    /// <param name="dayIndex">The current day</param>
    /// <param name="articleIndex">The article number that the player got wrong. Articles are numbered sequentially by day.</param>
    /// <param name="delay">The time we wait until the player recieves the email correcting them.</param>
    /// <returns></returns>
    #region Correction Emails

    public IEnumerator SendCorrectionEmailAfterDelay(int dayIndex, int articleIndex, float delay = 3.0f)
    {
        yield return new WaitForSeconds(delay);
        AddEmail(ReturnCorrectionEmail(dayIndex, articleIndex));
        CheckEmails();
    }
    /// <summary>
    /// Uses the day and article numbers to call the correct method to get the correction email.
    /// </summary>
    /// <param name="day"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public Email ReturnCorrectionEmail(int day, int index)
    {
        if (index < 1 || index > 10)
        {
            Debug.LogWarning($"An incorrect article index has been passed. Index: {index}");
            return null;
        }
        else if (day < 0 | day > 6)
        {
            Debug.LogWarning($"An incorrect day index has been passed. Day: {day}");
            return null;
        }

        //Days are 0 indexed right?
        switch (day)
        {
            case 0:
                return DayOneCorrections(index);
            case 1:
                return DayTwoCorrections(index);
            case 2:
                return DayThreeCorrections(index);
            case 3:
                return DayFourCorrections(index);
            case 4:
                return DayFiveCorrections(index);
            case 5:
                return DaySixCorrections(index);
            case 6:
                return DaySevenCorrections(index);
            default:
                return null;
        }
    }

    /// <summary>
    /// This is the method that actually generates all the text for the correction email. Called by each of the corrections methods.
    /// </summary>
    /// <param name="articleTitle">Title of article sorted  incorrectly</param>
    /// <param name="isReliable">Whether the article is reliable or not</param>
    /// <param name="pieceOfCRAAP">Which part of the CRAAP test the article pertains to</param>
    /// <param name="correctionInfo">Extra information needed for the CRAAP correction</param>
    /// <param name="hasTitle">Whether or not the article actually has a title</param>
    /// <returns></returns>
    public Email GenerateCorrectionEmail(string articleTitle, bool isReliable, string pieceOfCRAAP, string correctionInfo, bool hasTitle = true)
    {
        Email correctionEmail = gameObject.AddComponent<Email>();
        string reliableString = isReliable ? "unreliable, when it is actually reliable" : 
                                             "reliable, when it is actually unreliable";

        string titleOrBeginning = hasTitle ? $"titled \"{articleTitle}.\"" : $"which began {articleTitle}";

        correctionEmail.subject = $"Correction";
        correctionEmail.sender = "Boss"; //Change this later? Add story?
        correctionEmail.body = "Dear *,\n\nIt appears you have made an error " +
                               $"in regards to a recent article you reviewed {titleOrBeginning} " +
                               $"The article was marked incorrectly as {reliableString}.\n\n" +
                               $"The reliability of the information has to do with its {pieceOfCRAAP}. " +
                               $"{sortCRAAP(isReliable, pieceOfCRAAP, correctionInfo)}" +
                               $" Please have more diligence, and check the database if you are having trouble.";

        return correctionEmail;
    }

    /// <summary>
    /// Method returns a string that gives information pertaining to the piece of CRAAP for the article/email.
    /// </summary>
    /// <param name="isReliable">Whether or not the article was reliable</param>
    /// <param name="pieceOfCRAAP">The piece of CRAAP pertaining to the error</param>
    /// <param name="correctionInfo">Extra information given to the player about why they were wrong</param>
    /// <returns></returns>
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
    //All of these are hard coded. Sorry about that.
    public Email DayOneCorrections(int index)
    {
        switch (index)
        {
            case 1: //Counilman Cameron Rook steals candy from a baby //RELIABLE
                return GenerateCorrectionEmail("Councilman Cameron Rook steals candy from baby", true, "currency", "June 5th, 2019");
            case 2:
                return GenerateCorrectionEmail("C. Rook is a monster!", false, "currency", "January 6th, 2001");
            case 3:
                return GenerateCorrectionEmail("DO NOT VOTE FOR C. ROOK!…", false, "currency", "October 30th, 2013");
            case 4:
                return GenerateCorrectionEmail("Cameron Rook", true, "currency", "July 5th, 2020");
            case 5:
                return GenerateCorrectionEmail("C. Rook does WHAAAAAAT?!", false, "curency", "August 27th, 1999");
            //Part 2
            case 6:
                return GenerateCorrectionEmail("\"I used to babysit… \"", false, "relevancy", "it does not mention Cameron Rook, just a boy named Cameron", false);
            case 7:
                return GenerateCorrectionEmail("Investigation of Cameron Rook's involvement in the increase of Crime throughout Canton", true, "relevancy", "it has to do with C. Rook's legislation");
            case 8:
                return GenerateCorrectionEmail("C. Rook raises taxes, and forgives his own", true, "relevancy", "it discusses C. Rook's tax bill");
            case 9:
                return GenerateCorrectionEmail("\"Did you guys see the news… \"", false, "currency", "September 17th, 2009", false);
            case 10:
                return GenerateCorrectionEmail("Monsters Among Us", false, "relevancy", "it doesn't have to do with C. Rook's policies");
        }
        return null;
    }

    public Email DayTwoCorrections(int index)
    {
        switch (index)
        {
            case 1:
                return GenerateCorrectionEmail("Canton's Police Force, Void of Accountability", true, "authority", "published it in an academic journal");
            case 2:
                return GenerateCorrectionEmail("\"They've got the wrong guy!\"", false, "authority", "have no credentials or sources", false);
            case 3:
                return GenerateCorrectionEmail("BREAKING: State Attorneys confident in Police Sergeant’s innocence", false, "authority", "are politically motivated");
            //Part 2
            case 4:
                return GenerateCorrectionEmail("Canton Police Force", false, "relevancy", "it does not involve Kevin Iller");
            case 5:
                return GenerateCorrectionEmail("\"I have been inundated with questions…\"", true, "authority", "are a detective with a firsthand statement", false);
            case 6:
                return GenerateCorrectionEmail("We did it Saiddit!", false, "authority", "did not back up their statement with any credibility");
            case 7:
                return GenerateCorrectionEmail("Canton Clubber sleeping with Grand Mayor?!", false, "relevancy", "it does not have to do with the murder case");
            case 8:
                return GenerateCorrectionEmail("Marissa Urder", true, "authority", "are an archive of official documents");
            case 9:
                return GenerateCorrectionEmail("\"I would never have believed…\"", true, "authority", "directly quote a statement made by the chief of police", false);
            case 10:
                return GenerateCorrectionEmail("New evidence links suspect to Canton Clubber killings", false, "currency", "December 22nd, 2002");
        }
        return null;
    }

    public Email DayThreeCorrections(int index)
    {
        switch (index)
        {
            case 1:
                return GenerateCorrectionEmail("Water quality has never been better!", true, "accuracy", "was written by many experts");
            case 2:
                return GenerateCorrectionEmail("\"My whole family is freaking out…\"", false, "accuracy", "is not backed up by any real sources", false);
            case 3:
                return GenerateCorrectionEmail("Canton", true, "accuracy", "comes from a moderated information wiki");
            //Part 2
            case 4:
                return GenerateCorrectionEmail("Water quality has never been better!", false, "currency", "January 20th, 1980");
            case 5:
                return GenerateCorrectionEmail("Acid rain: Who to blame?", false, "authority", "are politically motivated");
            case 6:
                return GenerateCorrectionEmail("Today’s Acid Rain", true, "accuracy", "has a source backing it up");
            case 7:
                return GenerateCorrectionEmail("\"Everybody is complaining about their cars…\"", false, "relevancy", "brings up an issue unrelated to the acid rain", false);
            case 8:
                return GenerateCorrectionEmail("OMG, this acid rain has got to stop!", false, "accuracy", "does not confirm whether the problem was caused by the acid rain");
            case 9:
                return GenerateCorrectionEmail("Regulations of factories repealed, government forgets to inform the public.", true, "accuracy", "provides factual information");
            case 10:
                return GenerateCorrectionEmail("\"I can't believe people haven't realized it…\"", false, "accuracy", "does not provide any evidence for its statements", false);
        }
        return null;
    }

    public Email DayFourCorrections(int index)
    {
        switch (index)
        {
            case 1:
                return GenerateCorrectionEmail("Canton", true, "purpose", "give information about the Spanish Flu pandemic");
            case 2:
                return GenerateCorrectionEmail("Revisiting our history", false, "purpose", "make people think Canton is the greatest nation in the world");
            case 3:
                return GenerateCorrectionEmail("Canton’s Grand Mayors and their Contributions", false, "purpose", "make Canton's mayors look good");
            case 4:
                return GenerateCorrectionEmail("Canton’s industrial revolution", true, "purpose", "provide data on Canton's economy");
            case 5:
                return GenerateCorrectionEmail("Where did Canton come from?", false, "purpose", "glorify religion");
            //Part 2
            case 6:
                return GenerateCorrectionEmail("Centon is Awesome!", false, "relevancy", "does not provide any important information");
            case 7:
                return GenerateCorrectionEmail("Sociology of Canton: History of Ethics in the Workplace", false, "currency", "December 4th, 1964");
            case 8:
                return GenerateCorrectionEmail("Remembering the Downtown Inferno Crisis", true, "purpose", "provide information about a historical event");
            case 9:
                return GenerateCorrectionEmail("\" You know what, I’m gonna say it.\"", false, "accuracy", "does not provide any sources", false);
            case 10:
                return GenerateCorrectionEmail("Canton’s History of Conservation", true, "purpose", "provide information about Canton's efforts in nature conservation");
        }
        return null;
    }

    public Email DayFiveCorrections(int index)
    {
        switch (index)
        {
            case 1:
                return GenerateCorrectionEmail("I took all of my parent's money!", false, "relevancy", 
                    "despite having to do with the outcome of the hack, it gives no information about the attack itself");
            case 2:
                return GenerateCorrectionEmail("All eyes on you: How the government accesses your information", true, "CRAAP", "");
            case 3:
                return GenerateCorrectionEmail("Department of Information remarks on security of Canton’s data vaults", true, "CRAAP", "");
            case 4:
                return GenerateCorrectionEmail("Cash Catastrophe!", false, "accuracy", "is based on an opinion and not backed up with sources");
            case 5:
                return GenerateCorrectionEmail("\"I can't believe it!\"", false, "currency", "September 10th, 2001", false);
            case 6:
                return GenerateCorrectionEmail("\"All these beta males…\"", false, "purpose", "sell the influencer's program", false);
            case 7:
                return GenerateCorrectionEmail("PCs for the Public Program", true, "CRAAP", "");
            case 8:
                return GenerateCorrectionEmail("\"People of Canton…\"", true, "CRAAP", "", false);
            case 9:
                return GenerateCorrectionEmail("Code Cobras responsible for Data breach, Data vaults unaffected.", false, "purpose", 
                    "shield the government from any blame for the incident");
            case 10:
                return GenerateCorrectionEmail("\"All you worshippers…\"", false, "relevancy", "where Old man Cletus hides his money has nothing to do with this data breach", false);
        }
        return null;
    }

    public Email DaySixCorrections(int index)
    {
        switch (index)
        {
            case 1:
                return GenerateCorrectionEmail("The Cash Act", true, "CRAAP", "");
            case 2:
                return GenerateCorrectionEmail("\"Soon the Grand Mayor…\"", false, "relevancy", "it is advertising a giveaway", false);
            case 3:
                return GenerateCorrectionEmail("Concerns about the Cash Act", true, "CRAAP", "");
            case 4:
                return GenerateCorrectionEmail("\"I don't get what the hubbub…\"", false, "accuracy", "it does not accurately describe the consequences of the bill", false);
            case 5:
                return GenerateCorrectionEmail("Government owned businesses: How do they affect us?", false, "currency", "March 12th, 1984");
            case 6:
                return GenerateCorrectionEmail("What’s up with this Cash Act?", false, "authority", "are pursuing their own selfish goals");
            case 7:
                return GenerateCorrectionEmail("Finances and Fascism: Canton’s Economy Projections", true, "CRAAP", "");
            case 8:
                return GenerateCorrectionEmail("Canton’s Cash Act redistributes wealth at the sacrifice of many rights.", true, "CRAAP", "");
            case 9:
                return GenerateCorrectionEmail("The Cash Act to save the economy!", false, "purpose", "glorify the government");
            case 10:
                return GenerateCorrectionEmail("\"WE NEED MORE GOVERNMENT!\"", false, "purpose", "advocate for the government", false);
        }
        return null;
    }

    public Email DaySevenCorrections(int index)
    {
        switch (index)
        {
            case 1:
                return GenerateCorrectionEmail("\"I cannot believe…\"", false, "relevancy", "it makes an exaggerated claim", false);
            case 2:
                return GenerateCorrectionEmail("\"The Cash Act Bill is going…\"", false, "authority", "are an imposter account", false);
            case 3:
                return GenerateCorrectionEmail("Protest and Petition: The Importance of Public Demonstration Throughout Canton’s History", true, "CRAAP", "");
            case 4:
                return GenerateCorrectionEmail("What’s the big deal?", false, "accuracy", "tries to present an unlikely loophole");
            case 5:
                return GenerateCorrectionEmail("Government Conservation and its Flaws", true, "CRAAP", "");
            case 6:
                return GenerateCorrectionEmail("Government seizes factory, bankrupts it immediately", false, "currency", "March 13th, 2003");
            case 7:
                return GenerateCorrectionEmail("The Cash Act and how FMZ will go forward", true, "CRAAP", "");
            case 8:
                return GenerateCorrectionEmail("\"It only took a small nudge…\"", false, "purpose", "defend the actions of the Code Cobras", false);
            case 9:
                return GenerateCorrectionEmail("The unspoken benefits of Section XII", false, "purpose", "defend the Cash Act using amphiboly");
            case 10:
                return GenerateCorrectionEmail("Section XII and it’s Consequences", true, "CRAAP", "");
            //Part 2
            case 11:
                return GenerateCorrectionEmail("The People vs Kirstin Polluta", false, "relevancy", "it doesn't have to do specifically with the cash act or its effects");
            case 12:
                return GenerateCorrectionEmail("Kirstin Polluta Found Guilty, Town rejoices", false, "currency", "February 10th, 1935");
            case 13:
                return GenerateCorrectionEmail("Grand Mayor’s morality in question, compared to Kirstin Polluta.", true, "CRAAP", "");
            case 14:
                return GenerateCorrectionEmail("\" I find Peter Harbold…\"", false, "authority", "", false);
            case 15:
                return GenerateCorrectionEmail("\"To everyone criticizing the Grand Mayor…\"", false, "purpose", "crate false unity");
        }
        return null;
    }
    #endregion

    #region UI
    /// <summary>
    /// Sorts all the emails and removes empty spaces in the inbox to make everything nice and neat.
    /// </summary>
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
    /// <summary>
    /// Returns the number of the lowest empty place in the inbox.
    /// </summary>
    /// <returns></returns>
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
    /// Set buttons to NONE in navigation sto stop them from continuously calling their methods.
    /// Shows or hides the inbox panel.
    /// </summary>
    public void ShowHide()
    {
        DisplayEmails();
        inboxPanel.SetActive(!inboxPanel.activeSelf); //switch the active state
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
    /// <summary>
    /// Used to progress the game loop with the story emails and set the notification when the player has new emails.
    /// </summary>
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
    #endregion
}
