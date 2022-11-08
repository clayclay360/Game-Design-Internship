using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;
using UnityEngine;


public class Wiki : MonoBehaviour
{

    public GameObject Source;


    public void showSource()
    {
        if(Source != null)
        {
            bool isActive = Source.activeSelf;
            Source.SetActive(!isActive);
        }
    }
}
