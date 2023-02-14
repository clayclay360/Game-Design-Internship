using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameHolder : MonoBehaviour
{
    [Header("UI")]
    public InputField inputField;
    public Button startBtn;

    public CanvasGroup blackScreen;

    private void Start()
    {
        inputField.GetComponent<InputField>();
        startBtn.GetComponent<Button>();
    }

    public void CheckName()
    {
        //if the input field has characters then activiate the start btn else don't
        if(inputField.text != "")
        {
            startBtn.gameObject.SetActive(true);
        }
        else
        {
            startBtn.gameObject.SetActive(false);
        }
    }

    public void loadGame()
    {
        startBtn.gameObject.SetActive(false);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0;
        yield return new WaitForSeconds(2);

        while (blackScreen.alpha != 1)
        {
            blackScreen.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);

        Player.Name = inputField.text;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
