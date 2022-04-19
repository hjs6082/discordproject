using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Picture : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject album;
    FullPicture fullPicture;
    private Sprite sendSprite;
    [SerializeField]
    private GameObject fullPictureObj;
    [SerializeField]
    private Image fullPictureImage;

    [SerializeField]
    private UnityEngine.UI.Outline outline;

    private void Start()
    {
        fullPicture = GetComponent<FullPicture>();
        sendSprite = this.gameObject.GetComponent<Image>().sprite;
    }

    public void PictureClick()
    {
        FullPicture.clickAction += PictureSend;
        fullPictureObj.SetActive(true);
    }
    
    public void PictureSend()
    {
        PlayPicture(sendSprite);
    }

    public void PlayPicture(Sprite _sprite)
    {
        fullPictureImage.GetComponent<Image>().sprite = _sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline = this.gameObject.GetComponent<UnityEngine.UI.Outline>();
        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 128);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 0);
    }

    /*    IEnumerator RotateAndPlay(Sprite _sprite) // 닷트윈 삭제로 인하여 쓰지않습니다.
        {   
            Cursor.lockState = CursorLockMode.Locked;
            fullPictureImage.GetComponent<Image>().sprite = _sprite;
            fullPictureObj.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 1.5f);
            yield return new WaitForSeconds(1.5f);
            Debug.Log("423");
            Cursor.lockState = CursorLockMode.None;
            album.SetActive(false);
        }*/
}
