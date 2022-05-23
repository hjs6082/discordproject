using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordPaperUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject passwordPanel;
    public GameObject player;
    public GameObject blurPanel;

    private void OnEnable()
    {
        blurPanel.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
            blurPanel.SetActive(false);
            player.GetComponent<Suntail.PlayerController>().enabled = true;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.gameObject.SetActive(false);
        blurPanel.SetActive(false);
        player.GetComponent<Suntail.PlayerController>().enabled = true;

    }


}
