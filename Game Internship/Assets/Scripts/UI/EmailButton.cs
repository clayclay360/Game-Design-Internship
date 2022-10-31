using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailButton : Button
{
    public EmailSystem emailsys;
    public override void OnClick()
    {
        if (emailsys)
        {
            emailsys.gameObject.SetActive(!emailsys.gameObject.activeSelf);
        }
    }
}
