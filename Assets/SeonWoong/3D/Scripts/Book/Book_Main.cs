using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ePage
{
    LIST   = 0,
    ALBUM,
    MAP,
    OPTION
};

public class Book_Main : MonoBehaviour
{
    private const float ON_POS_Y  = 0.0f;
    private const float OFF_POS_Y = -1075.0f;

    private RectTransform bookTrm = null;

    public  GameObject[] book_Page_Arr   = null;
    public  Button[]     book_Button_Arr = null;

    public ePage oldPage = ePage.LIST;
    public ePage curPage = ePage.LIST;

    private void Awake()
    {
        bookTrm = GetComponent<RectTransform>();
    }

    private void Start()
    {
        bookTrm.anchoredPosition3D = new Vector3(0, OFF_POS_Y, 0);

        InitButtons();
    }

    private void OnEnable()
    {
        //GameManager.Instance.Book = GetComponent<Book_Main>();
    }

    private void InitButtons()
    {
        for(int i = 0; i < book_Button_Arr.Length; i++)
        {
            int idx = i;

            book_Button_Arr[idx].onClick.AddListener(() => 
            {
                FlipButton(idx);
            });
        }
    }

    public void FlipButton(int _idx)
    {
        ChangePage((ePage)(_idx));
        //Debug.Log((ePage)((_idx + 1) * 2));
        //InitBtnSize(_idx);

        NextPage();
                
        // int pageCount = (int)oldPage - (int)curPage;
        // //Debug.Log(pageCount);

        // if(pageCount != 0 && !isFlipping)
        // {
        //     isFlipping = true;

        //     if (pageCount > 0)
        //     {
        //         // 왼쪽으로 flipk
        //     }
        //     else
        //     {
        //         // 오른쪽으로 flip
        //     }

        //     Debug.Log(book_Flip.controledBook.currentPage);

        //     NextPage();
        // }
        // else
        // {
        //     Debug.Log("응애");
        // }
    }

    private void InitBtnSize(int _idx)
    {
        for(int i = 0; i < book_Button_Arr.Length; i++)
        {
            bool isBig = (i == _idx) ? true : false;

            RectTransform rectTrm = book_Button_Arr[i].GetComponent<RectTransform>();
            Vector2 vt = rectTrm.sizeDelta;

            if(isBig) { vt = new Vector2(120, 120); }
            else      { vt = new Vector2(120,  80); }

            rectTrm.sizeDelta = vt;
        }
    }

    private void ChangePage(ePage _page)
    {
        oldPage = curPage;
        curPage = _page;
    }

    private void NextPage()
    {
        int page = (int)curPage;
        bool active = false;

// 고쳐야함
        for(int i = 0; i < book_Page_Arr.Length; i++)
        {
            active = (i == page) ? true : false;
            //Debug.Log(active);
            book_Page_Arr[i].SetActive(active);
        }
    }

    public void OnOffBook(bool _isOn)
    {
        float endValue = _isOn ? ON_POS_Y : OFF_POS_Y;

        bookTrm.DOAnchorPosY(endValue, 0.5f)
        .OnComplete(() => 
        {
            Debug.Log("Rmx");
        });
    }
}
