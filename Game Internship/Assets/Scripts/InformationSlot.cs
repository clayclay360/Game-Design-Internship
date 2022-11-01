using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InformationSlot : MonoBehaviour, IDropHandler
{
    public enum SlotType { reliable, unreliable };
    public SlotType slotType;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Information Dropped");
        if(eventData.pointerDrag != null)
        {
            Information information = eventData.pointerDrag.GetComponent<Information>();

            if (information != null)
            {
                if(information.isReliable && slotType.Equals(SlotType.reliable))
                {
                    Debug.Log("Correct");
                }
                else if(!information.isReliable && slotType.Equals(SlotType.unreliable))
                {
                    Debug.Log("Correct");
                }
                else
                {
                    Debug.Log("Incorrect");
                }

                Destroy(information.gameObject);
                GameManager.readyForNextSource = true;
            }
        }
    }
}
