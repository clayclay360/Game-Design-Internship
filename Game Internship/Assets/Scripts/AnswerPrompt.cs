using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPrompt : MonoBehaviour
{
    Dictionary<int,int> answers = new Dictionary<int,int>();

    // Start is called before the first frame update
    void Start()
    {
        answers.Add(0, 1);   
    }

    //get the button that is pressed and check whether it's correct.
    public void GetButton(int num)
    {
        if (answers[GameManager.currentDay] == num)
        {
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("incorrect");
        }

        FindObjectOfType<Main>().EndDay();
        gameObject.SetActive(false);
    }
}
