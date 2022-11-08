using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailSystem : MonoBehaviour
{
    public Email currentEmail;
    [Header("UI")]
    public GameObject emailPanel;
    public GameObject inboxPanel;
    public Text buttonText;
    public Button toggleBtn;
    public Email[] Inbox;

    [Header("Inbox")]
    public Button[] InboxBtns;
    public Text[] InboxSubjects;
    public Text[] InboxFroms;

    [Header("Email Parts")]
    public Text subject;
    public Text sender;
    public Text body;
  

    private void Awake()
    {
        Inbox = new Email[5];
        ShowHide();
        DisplayEmails();
    }

    public void DisplayEmails()
    {
        SortInbox();
        for (int i = 0; i < Inbox.Length; i++)
        {
            if (Inbox[i])
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


    public void ShowHide()
    {
        DisplayEmails();
        inboxPanel.SetActive(!inboxPanel.activeSelf); //switch the active state
        buttonText.text = inboxPanel.activeSelf ? "Hide Inbox" : "Show Inbox";
    }

    public void ShowEmail(int btnNum) //Accessed from button
    {
        ChangeEmail(Inbox[btnNum]);
        emailPanel.SetActive(true);
    }

    public void HideEmail() //Accessed from button
    {
        emailPanel.SetActive(false);
    }

}
