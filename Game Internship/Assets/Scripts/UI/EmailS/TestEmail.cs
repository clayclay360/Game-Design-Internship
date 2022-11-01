using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEmail : Email
{
    public TestEmail()
    {
        subject = "Vote for Mayor Floudners!";
        sender = "Committee to re-elect Mayor Flounders";
        body = "Can Mayor Flounders count on your support in the upcoming election? He could really use it.";
    }
}
