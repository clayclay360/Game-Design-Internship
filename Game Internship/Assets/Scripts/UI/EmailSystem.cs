using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailSystem : MonoBehaviour
{
    public Email currentEmail;
    [Header("Email Parts")]
    public Text subject;
    public Text sender;
    public Text body;

    public void ChangeEmail(Email email)
    {
        subject.text = email.subject;
        sender.text = email.sender;
        body.text = email.body;
    }

    private void Update()
    {
        if (currentEmail)
        {
            ChangeEmail(currentEmail);
        }
    }
}
