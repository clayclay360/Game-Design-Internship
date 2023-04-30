using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormLink : MonoBehaviour
{    public void OpenURL()
    {
        Application.OpenURL("https://forms.office.com/r/qxvdHVKJqb");
        Debug.Log("is this working?");
    }

}
