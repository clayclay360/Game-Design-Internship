using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InformationSlot : MonoBehaviour, IDropHandler
{
    public enum SlotType { reliable, unreliable };
    public SlotType slotType;
    public EmailSystem emailSys;


    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            Information information = eventData.pointerDrag.GetComponent<Information>();

            if (information != null)
            {
                if(information.isReliable && slotType.Equals(SlotType.reliable))
                {
                    GameManager.correctlySorted += 1;
                    GameManager.playerScore += 50; // player score increases
                    GameManager.numberOfCorrectSources++; // number of correct sources playet gets
                }
                else if(!information.isReliable && slotType.Equals(SlotType.unreliable))
                {
                    GameManager.correctlySorted += 1;
                    GameManager.playerScore += 50; // player score increases 
                    GameManager.numberOfCorrectSources++; // number of correct sources playet gets
                }
                else
                {
                    //incorrect
                    StartCoroutine(emailSys.SendCorrectionEmailAfterDelay(GameManager.currentDay, information.index));
                }

                
                Destroy(information.gameObject);
                GameManager.readyForNextSource = true;
            }
        }
    }
}
