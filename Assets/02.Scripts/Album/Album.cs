using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Album : MonoBehaviour
{
    private void OnEnable()
    {
        AlbumUp();
    }

    void AlbumUp()
    {
        this.gameObject.GetComponent<RectTransform>().DOLocalMoveX(3f, 3f, false);
        this.gameObject.GetComponent<RectTransform>().DOLocalMoveY(4f, 3f, false);
    }
}
