using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class SearchManager : MonoBehaviour
{
    public GameObject ContentHolder;
    public GameObject[] Element;
    public GameObject SearchBar;
    public GameObject SearchBTN;
    public GameObject WikiBTN;

    public int totalElements;

    void Start()
    {
        SearchBTN.SetActive(false);
        SearchBar.SetActive(false);
        WikiBTN.SetActive(false);
        StartCoroutine(showSearch());
        totalElements = ContentHolder.transform.childCount;
        Element = new GameObject[totalElements];

        for(int i = 0; i<totalElements; i++)
        {
            Element[i] = ContentHolder.transform.GetChild(i).gameObject;
        }


    }

    public void Search()
    {
        string SearchText = SearchBar.GetComponent<InputField>().text;
        int searchTxtLength = SearchText.Length;
        int searchedElements = 0;

        foreach(GameObject ele in Element)
        {
            searchedElements += 1;

            if (ele.transform.GetChild(0).GetComponent<Text>().text.Length >= searchTxtLength)
            {
                if(SearchText.ToLower() == ele.transform.GetChild(0).GetComponent<Text>().text.Substring(0, searchTxtLength).ToLower())
                {
                    ele.SetActive(true);
                }
                else
                {
                    ele.SetActive(false);
                    
                }
            }

        }
        
    }

    public IEnumerator showSearch()
    {
        yield return new WaitForSeconds(3f);
        SearchBTN.SetActive(true);
        SearchBar.SetActive(true);
        WikiBTN.SetActive(true);
    }
}
