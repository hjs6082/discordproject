using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AlbumImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private UnityEngine.UI.Outline outline;
    [SerializeField]
    private GameObject fullPictureObj;
    [SerializeField]
    private GameObject album;

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline = this.gameObject.GetComponent<UnityEngine.UI.Outline>();
        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 128);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 0);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }

    public void Close()
    {
        fullPictureObj.SetActive(false);
        album.SetActive(true);
    }

/*    IEnumerator Close() 이곳에 닷트윈을 쓰지 않음으로 인하여 폐기
    {
        Cursor.lockState = CursorLockMode.Locked;
        fullPictureObj.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, -20f), 1.5f);
        yield return new WaitForSeconds(1.5f);
        album.SetActive(true);
        fullPictureObj.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }*/
}
