using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public void StartLevel()
    {
        int randomQuestionIndex = GameManager.reliableInformation.Count;

        switch (randomQuestionIndex)
        {
            case 0:
                GameManager.question = "Is the sky purple?";
                break;
            case 1:
                GameManager.question = "Are turtles slow?";
                break;
        }
    }
}
