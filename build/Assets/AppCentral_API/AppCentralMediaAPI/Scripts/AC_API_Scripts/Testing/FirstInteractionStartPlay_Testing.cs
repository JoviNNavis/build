using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AppCentralAPI;

namespace AppCentralTesting
{
    public class FirstInteractionStartPlay_Testing : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            //AppCentral.StartPlay();
            //GetComponent<Button>().interactable = false;
        }
    }
}
