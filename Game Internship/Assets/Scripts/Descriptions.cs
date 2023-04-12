using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descriptions : MonoBehaviour
{
    public GameObject Source;
    public GameObject Content;
    public GameObject ExitBTN;

    void Start()
    {
        Content.SetActive(false);
        Source.SetActive(false);
        ExitBTN.SetActive(false);
    }
    public void ShowDescription()
    {
        if (Source != null)
        {
            bool isActive = Source.activeSelf;
            Source.SetActive(!isActive);
            Debug.Log(isActive);
            Content.SetActive(false);
            ExitBTN.SetActive(true);

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

    public void showWiki()
    {
        bool isActive = Content.activeSelf;
        Content.SetActive(!isActive);


    }
}
