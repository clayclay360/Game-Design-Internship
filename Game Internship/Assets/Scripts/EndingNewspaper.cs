using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This is for the Newspaper shown at the end of every day.
public class EndingNewspaper : MonoBehaviour
{
    //Actual string variables for the newpaper elements. I'm not sure if these are used but I'm too afraid to comment them out.
    [Header("Variables")]
    public string headlineText;
    public string deckText;
    public string articleText;

    public Animation spinAnimation; //The animation the plays whenever the newspaper appears.
    //References to the UI objects that we are setting
    [Header("UI")]
    public Text headline;
    public Text deck;
    public Text article;

    /// <summary>
    /// Sets the newspaper elements give
    /// </summary>
    /// <param name="hl">The headline of the article</param>
    /// <param name="dck">Not the deck, but the newspaper title since the rework</param>
    /// <param name="txt">The content of the article</param>
    /// <param name="sprite">Not used. We used to have an image along with the newspaper, but no longer</param>
    public void SetNewspaper(string hl, string dck, string txt, Sprite sprite = null)
    {
        headline.text = hl;
        deck.text = dck;
        article.text = txt;
        //imageRend.sprite = sprite == null ? imageRend.sprite : sprite; //Change the sprite if not null
    }
}
