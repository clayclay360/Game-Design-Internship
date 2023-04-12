using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descriptions : MonoBehaviour
{
    public GameObject Source;
    public GameObject Content;
    public GameObject ExitBTN;
    public void ShowDescription()
    {
        if (Source != null)
        {
            bool isActive = Source.activeSelf;
            Source.SetActive(!isActive);
            Debug.Log(isActive);
            ExitBTN.SetActive(true);
            Content.SetActive(false);

        }
    }

    public void GetNameList()
    {
        if(Source != null)
        {
            bool isActive = Source.activeSelf;
            Source.SetActive(!isActive);
            Debug.Log(isActive);
        }
        Content.SetActive(true);
        ExitBTN.SetActive(false);

        
        
    }
}
