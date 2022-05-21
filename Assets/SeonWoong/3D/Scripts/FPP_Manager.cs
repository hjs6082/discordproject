using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FPP_Manager : MonoBehaviour
{
    #region 싱글톤
    public  static FPP_Manager Instance
    {
        get
        {
            return instance;
        }
    }
    private static FPP_Manager instance = null;
    #endregion

    private static readonly Vector3 DEFAULT_PLAYER_POS = new Vector3(10.0f, 3.1f, 3.5f);
    private static readonly Vector3 DEFAULT_PLAYER_ROTATE = new Vector3(0.0f, 180.0f, 0.0f);

    private List<string> manage_Strs_List;

    private int curChapter = 0;

    public Texture2D[] cursor_Textures;

    public  GameObject houseKey  = null;
    private Transform  player    = null;
    private Sequence   player_SQ = null;

    private FPP_Move    fpp_Move = null;
    private FPP_Control fpp_Ctrl = null;

    public  Text fpp_Speech_Text = null;
    public  bool bUpDownBtn = false;
    private bool bWait  = false;
    private int  talkCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        InitValue();

        OnOffText(false);

    }

    private void Start()
    {
        InitPlayer();

        curChapter = (int)GameManager.Instance.curChapter;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        FPP_MouseCursor.ChangeCursor(cursor_Textures[0], false);
    }

    private void Update()
    {

    }

    private void InitValue()
    {
        player = this.transform;
     
        fpp_Move = GetComponent<FPP_Move>();
        fpp_Ctrl = GetComponent<FPP_Control>();

        manage_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.FPP_MANAGER_STRS);

        houseKey.SetActive(false);
    }

    private void InitPlayer()
    {
        player.position = DEFAULT_PLAYER_POS;
        player.rotation = Quaternion.Euler(DEFAULT_PLAYER_ROTATE);

        StartMove();
    }

    private void StartMove()
    {
        fpp_Move.bObject = true;

        player_SQ = DOTween.Sequence();

        player_SQ.Append(
            player.DOMoveY(3.3f, 0.5f)
            .SetEase(Ease.OutQuad));
        player_SQ.Join(
            player.DOMoveZ(4.0f, 0.5f)
            .SetEase(Ease.InQuad));

        player_SQ.Play()
        .SetDelay(0.5f)
        .OnComplete(() => 
        {
            switch(curChapter)
            {
                case 0:
                {
                   StartTalk(manage_Strs_List[talkCount]);
                }
                break;
                case 1:
                {
                    
                }
                break;
                case 2:
                {

                }
                break;
                case 3:
                {

                }
                break;
                default:
                break;
            }
        });
    }
    
    public void EndMove(Action _callback = null)
    {
        player.DOMove(DEFAULT_PLAYER_POS, 0.5f)
        .SetEase(Ease.OutQuad)
        .OnComplete(() => 
        {
            _callback?.Invoke();
        });
    }

    public void FindObjectTalk(string _str, Action _callback = null, FontStyle _fontStyle = FontStyle.Normal)
    {
        ClearText();

        fpp_Speech_Text.fontStyle = _fontStyle;
        fpp_Speech_Text.DOText(_str, _str.Length * 0.1f)
        .OnComplete(() => 
        {
            _callback?.Invoke();
        });
    }

    public void OnOffText(bool _bOn)
    {
        fpp_Speech_Text.transform.parent.gameObject.SetActive(_bOn);
    }

    private void ClearText()
    {
        fpp_Speech_Text.text = string.Empty;
    }

    public FPP_Move GetMove()
    {
        return fpp_Move;
    }

    private void StartTalk(string _str)
    {
        bWait = true;

        OnOffText(true);
        StartCoroutine(FastTalk());

        FindObjectTalk(_str, () =>
        {
            StartCoroutine(NextTalk(() =>
            {
                if (talkCount < manage_Strs_List.Count - 1)
                {
                    talkCount++;
                    StartTalk(manage_Strs_List[talkCount]);
                }
                else
                {
                    CheckLists.AddCheckList(CheckLists.FPP_CHECKLIST_STRS[0]);
                    fpp_Move.bObject = false;
                    OnOffText(false);
                }
            }));
        });
    }

    private IEnumerator FastTalk()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 5.0f;
                break;
            }

            yield return null;
        }
    }

    private IEnumerator NextTalk(Action _onComplete)
    {
        while(bWait)
        {
            if(Input.GetMouseButtonDown(0))
            {
                bWait = false;

                Time.timeScale = 1.0f;
            }

            yield return null;
        }

        _onComplete?.Invoke();
        
    }
}
