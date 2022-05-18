using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPhotoScript : MonoBehaviour//, IPointerClickHandler
{
    public GameObject player;
    public GameObject clearPanel;
    /*    public void OnPointerClick(PointerEventData eventData)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
            player.GetComponent<Suntail.PlayerController>().enabled = true;
        }*/

    public void OnEnable()
    {
        StartCoroutine(Chapter1Clear());
    }

    IEnumerator Chapter1Clear()
    {
        yield return new WaitForSeconds(5f);
        clearPanel.SetActive(true); //Ŭ�����г��̾ƴ϶� ���̵����� �ٲܰ�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Suntail.PlayerController>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
