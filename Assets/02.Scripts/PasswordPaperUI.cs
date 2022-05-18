using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordPaperUI : MonoBehaviour//, IPointerClickHandler
{
    // public Item photoData;
    public GameObject clearPanel;
    public GameObject player;
    //public GameObject photoObj;
    /*    public void OnPointerClick(PointerEventData eventData)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
            Inventory.instance.AddItem(photoData);
            player.GetComponent<Suntail.PlayerController>().enabled = true;
            Inventory.instance.FreshSlot();
            photoObj.SetActive(false);
        }*/

    public void OnEnable()
    {
        StartCoroutine(Chapter1Clear());
    }

    IEnumerator Chapter1Clear()
    {
        yield return new WaitForSeconds(5f);
        clearPanel.SetActive(true); //클리어패널이아니라 씬이동으로 바꿀것
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Suntail.PlayerController>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
