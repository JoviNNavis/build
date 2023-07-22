using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AppCentralAPI;

namespace AppCentralTesting
{
    public class FirstInteractionDeepInGame_Testing : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            AppCentral.DeepInGame();
            GetComponent<Button>().interactable = false;
        }
    }
}