using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DraggableUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool scalable;
    
    public Canvas canvas;
    

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup =  GetComponent<CanvasGroup>();

        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        rectTransform.localScale /= 1.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        rectTransform.localScale *= 1.5f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scalable)
        {
            rectTransform.localScale *= 1.25f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (scalable)
        {
            rectTransform.localScale /= 1.25f;
        }
    }
}
