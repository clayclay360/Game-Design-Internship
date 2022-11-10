using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Source
{
    public enum type { article, social, research, newspaper};
    public type Type;
    public bool isReliable;
    public string source;
    public string title;
    public string author;
    [TextArea(1,8)]
    public string information;
    public string month;
    public int year;
}
