using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monitor : MonoBehaviour
{
    private void OnMouseEnter()
    {
        
        if (GameManager.Instance.Book.gameObject.activeSelf && Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.5f)
        {

        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.Book.gameObject.activeSelf && Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.5f)
        {
            FPP_Manager.Instance.EndMove(() =>
            {
                GameManager.Instance.Fade_Out(0.5f, () =>
                {
                    LoadScene.LoadingScene("TestMap");
                }, Ease.Linear, Color.white);
            });
        }
    }
}
