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

    public GameObject check_Prefab = null;
    public Transform checkList_Parent = null;
    public List<GameObject> checkList_List = new List<GameObject>();
    public Image background = null;
    private RectTransform bookTrm = null;

    public  GameObject[] book_Page_Arr   = null;
    public  Button[]     book_Button_Arr = null;
    public  Image[]     book_Button_Back_Arr = null;

    public ePage oldPage = ePage.LIST;
    public ePage curPage = ePage.LIST;

    private void Awake()
    {
        bookTrm = GetComponent<RectTransform>();

        InitButton();
        InitBook();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        //GameManager.Instance.Book = GetComponent<Book_Main>();
    }

    private void InitButton()
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

    private void InitBook()
    {
        bookTrm.anchoredPosition3D = new Vector3(0, OFF_POS_Y, 0);
    }

    public void FlipButton(int _idx)
    {
        ChangePage((ePage)(_idx));
        InitBtnSize(_idx);
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

            book_Button_Back_Arr[i].gameObject.SetActive(isBig);
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
        Sequence seq = DOTween.Sequence();

        float endValue = _isOn ? ON_POS_Y : OFF_POS_Y;
        float fadeValue = _isOn ? 100.0f : 0.0f;

        seq.Append(
        bookTrm.DOAnchorPosY(endValue, 0.5f)
        .OnComplete(() => 
        {
            Debug.Log("Rmx");
        })
        );

        seq.Join(
            background.DOFade(fadeValue / 255.0f, 0.5f)
        );

        seq.Play();
    }
}
