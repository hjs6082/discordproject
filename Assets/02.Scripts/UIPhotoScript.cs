using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIPhotoScript : MonoBehaviour//, IPointerClickHandler
{
    public GameObject player;
    public GameObject clearPanel;
    public GameObject blinkPanel;
    public GameObject blinkPanel2;

    private int blinkCount = 0;
    /*    public void OnPointerClick(PointerEventData eventData)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
            player.GetComponent<Suntail.PlayerController>().enabled = true;
        }*/

    public void OnEnable()
    {
        //StartCoroutine(Chapter1Clear());
        StartCoroutine(Blink());
    }

/*    IEnumerator Chapter1Clear()
    {

        yield return new WaitForSeconds(8f);


    }*/

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(1.5f);
        if (blinkCount < 3)
        {
            blinkPanel.transform.DOLocalMoveY(497f, 0.8f).OnComplete(() => { blinkPanel.transform.DOLocalMoveY(1164f, 0.8f); });
            blinkPanel2.transform.DOLocalMoveY(-599f, 0.8f).OnComplete(() => { blinkPanel2.transform.DOLocalMoveY(-1357f, 0.8f); });
            blinkCount++;
        }
        else
        {
            blinkPanel.transform.DOLocalMoveY(497, 0.8f).OnComplete(() => { EndPanel(); });
            blinkPanel2.transform.DOLocalMoveY(-599, 0.8f).OnComplete(() => { EndPanel(); });

            StopCoroutine(Blink());
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Blink());
    }

    public void EndPanel()
    {
        clearPanel.SetActive(true); //클리어패널이아니라 씬이동으로 바꿀것
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Suntail.PlayerController>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
