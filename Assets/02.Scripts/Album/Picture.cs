using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Picture : MonoBehaviour
{
    [SerializeField]
    private GameObject album;

    FullPicture fullPicture;
    private Sprite sendSprite;
    [SerializeField]
    private GameObject fullPictureObj;
    [SerializeField]
    private Image fullPictureImage;

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
        StartCoroutine(RotateAndPlay(sendSprite));
    }

/*    public void RotateAndPlay(Sprite _sprite)
    {

    }*/

    IEnumerator RotateAndPlay(Sprite _sprite)
    {
        Cursor.lockState = CursorLockMode.Locked;
        fullPictureImage.GetComponent<Image>().sprite = _sprite;
        fullPictureObj.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 1.5f);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("423");
        Cursor.lockState = CursorLockMode.None;
        album.SetActive(false);
    }
}
