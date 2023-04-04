using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descriptions : MonoBehaviour
{
    public GameObject Source;
    public void ShowDescription()
    {
        if (Source != null)
        {
            bool isActive = Source.activeSelf;
            Source.SetActive(!isActive);

        }
    }
}
