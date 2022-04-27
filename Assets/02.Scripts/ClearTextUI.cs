using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClearTextUI : MonoBehaviour, IPointerClickHandler
{
    public static bool isBlueKeyText = false;
    //public GameObject player;
    public void OnPointerClick(PointerEventData eventData)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.SetActive(false);
        isBlueKeyText = true;
        // player.GetComponent<Suntail.PlayerController>().enabled = true;
    }
}
