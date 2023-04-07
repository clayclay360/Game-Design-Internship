using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Information : MonoBehaviour
{
    [Header("UI")]
    public Text sourceText;
    public Text titleText;
    public Text authorText;
    public Text informationText;
    public Text monthText;
    public Text yearText;
    [HideInInspector]
    public bool isReliable;
    public int index;
}
