using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Monitor : MonoBehaviour
{
    private Book_Main book_Main = null;

    private void Start()
    {
        book_Main = GameManager.Instance.book.GetComponent<Book_Main>();
    }

    private void OnMouseEnter()
    {

        if (GameManager.Instance.book.activeSelf && Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.5f)
        {

        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.book.activeSelf && Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.5f)
        {
            
        }
    }
}
