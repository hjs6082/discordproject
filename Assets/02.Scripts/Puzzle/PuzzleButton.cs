using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleButton : MonoBehaviour
{
    public GameObject map;
    public Text startText;
    public Text wrongText;
    public Text okText;

    [SerializeField]
    MovePuzzle mp;

    public bool show = false;

    public void Start()
    {
        //mp.Shuffle();
    }
    public void Show()
    {
        if (show == false)
        {
            map.SetActive(true);
            show = true;
        }
        else
        {
            map.SetActive(false);
            show = false;
        }
    }

    public void ResetButton()
    {
        mp.Shuffle();
        mp.puzzleStart = true;
        startText.gameObject.SetActive(false);
    }

    public void ClearCheckButton()
    {
        mp.ClearCheck();
        if(mp.isWrong)
        {
            StartCoroutine(wrongDelete(2f));
        }
        if(mp.isClear)
        {
            StartCoroutine(OK(2));
        }
    }

    IEnumerator wrongDelete(float second)
    {
        wrongText.gameObject.SetActive(true);
        mp.isWrong = false;
        yield return new WaitForSeconds(second);
        wrongText.gameObject.SetActive(false);
    }

    IEnumerator OK(float second)
    {
        okText.gameObject.SetActive(true);
        GameManager.Instance.bMovePuzzleClear = true;
        GameManager.Instance.SavePuzzle();
        yield return new WaitForSeconds(second);
        okText.gameObject.SetActive(false);
        SceneManager.LoadScene("MoveScene");
    }
}
