using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPrompt : MonoBehaviour
{
    Dictionary<int,int> answers = new Dictionary<int,int>(); //<Day, Correct Answer #>

    // Start is called before the first frame update
    void Start()
    {
        answers.Add(0, 1); //Day 1, Correct answer: choice 2
        answers.Add(1, 1); //Day 2, Correct answer: choice 2
    }

    //get the button that is pressed and check whether it's correct.
    public void GetButton(int num)
    {
        //`answers[GameManager.currentDay] == num` checks if we selected the
        //correct option, returning true or false
        FindObjectOfType<Main>().EndDay(answers[GameManager.currentDay] == num);
        gameObject.SetActive(false);
    }
}
