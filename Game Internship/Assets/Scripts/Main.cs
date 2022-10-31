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

    private void Update()
    {
        CheckPlayerInput();
    }

    public void CheckPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                hit.transform.gameObject.TryGetComponent<Button>(out Button uibutt);
                uibutt.OnClick();
            }
        }
    }
}
