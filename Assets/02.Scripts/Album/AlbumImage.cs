using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AlbumImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Outline outline;
    [SerializeField]
    private GameObject fullPictureObj;
    [SerializeField]
    private GameObject album;

    public void OnPointerEnter(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        fullPictureObj.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, -20f), 1.5f);
        yield return new WaitForSeconds(1.5f);
        album.SetActive(true);
        fullPictureObj.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
}
