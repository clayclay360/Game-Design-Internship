using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup blackScreen;
    public Text questionText;
    public GameObject draggableUI;

    [Header("Questions")]
    public Question[] questions;

    private int sourceIndex;

    private InformationType informationType;
    private void Start()
    {
        informationType = GetComponent<InformationType>();

        StartCoroutine(Gameloop());
    }

    public IEnumerator Gameloop()
    {
        // start the day and display question
        #region Display
        blackScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        while(blackScreen.alpha != 0)
        {
            blackScreen.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);
        questionText.text = questions[GameManager.currentDay].question;
        #endregion

        // start passing out information
        yield return null;
        int numberOfReviewedInformation = 0;
        while(numberOfReviewedInformation < questions[GameManager.currentDay].sources.Length)
        {
            //temporary
            sourceIndex = numberOfReviewedInformation;
            CreateSource(questions[GameManager.currentDay].sources[sourceIndex].Type);
            numberOfReviewedInformation++;
        }
        Debug.Log("End Task");

    }

    public void CreateSource(Source.type sourceType)
    {
        GameObject informationPrefab = null;

        switch (sourceType)
        {
            case Source.type.article:
                informationPrefab = Instantiate(informationType.articlePrefab, draggableUI.transform);
                break;
            case Source.type.social:
                informationPrefab = Instantiate(informationType.socialsPrefab, draggableUI.transform);
                break;
            case Source.type.research:
                break;
        }

        Information information = informationPrefab.GetComponent<Information>();
        information.sourceText.text = questions[GameManager.currentDay].sources[sourceIndex].source;
        information.titleText.text = questions[GameManager.currentDay].sources[sourceIndex].title;
        information.authorText.text = questions[GameManager.currentDay].sources[sourceIndex].author;
        information.informationText.text = questions[GameManager.currentDay].sources[sourceIndex].information;
        information.monthText.text = questions[GameManager.currentDay].sources[sourceIndex].month;
        information.yearText.text = questions[GameManager.currentDay].sources[sourceIndex].year.ToString();
    }
}
