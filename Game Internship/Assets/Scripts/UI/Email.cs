using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email : MonoBehaviour
{
    public string sender;
    public string subject;
    [TextArea(4,50)]public string body;
    public bool opened;
}
