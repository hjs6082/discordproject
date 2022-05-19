using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            for(int i = 0; i < GameManager.Instance.Book.checkList_List.Count; i++)
            {
                if(GameManager.Instance.Book.checkList_List[i].GetComponentInChildren<Text>().text == "컴퓨터로 다시 돌아가기")
                {
                    GameManager.Instance.Book.checkList_List[i].GetComponent<Toggle>().isOn = true;
                }
            }

            FPP_Manager.Instance.EndMove(() =>
            {
                GameManager.Instance.Fade_Out(0.5f, () =>
                {
                    LoadScene.LoadingScene("MapJunseo");
                }, Ease.Linear, Color.white);
            });
        }
    }
}
