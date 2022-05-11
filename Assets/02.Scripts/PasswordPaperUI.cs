using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordPaperUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject player;
    public void OnPointerClick(PointerEventData eventData)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.SetActive(false);
        player.GetComponent<Suntail.PlayerController>().enabled = true;
    }
}
