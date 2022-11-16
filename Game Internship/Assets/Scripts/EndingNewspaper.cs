using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingNewspaper : MonoBehaviour
{
    [Header("Variables")]
    public string headlineText;
    public string deckText;
    public string articleText;
    public Sprite image;

    [Header("UI")]
    public TextMeshProUGUI headline;
    public TextMeshProUGUI deck;
    public TextMeshProUGUI article;
    public Image imageRend;

    //I don't expect this to work completely with the newspaper layout but it could come in handy.
    public void SetNewspaper(string hl, string dck, string txt, Sprite sprite = null)
    {
        headline.text = hl;
        deck.text = dck;
        article.text = txt;
        //imageRend.sprite = sprite == null ? imageRend.sprite : sprite; //Change the sprite if not null
    }
}
