using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DrawerOpen : MonoBehaviour
{
    [SerializeField]
    private Text explaneText; 
    [SerializeField]
    private Image uiPanel;
    private bool isEnter;
    public bool isCheck;
    private Vector3 startPosition;
    private Vector3 goalPosition;

    public bool isEnd;
    private void Awake()
    {
        startPosition = this.gameObject.transform.position;
        goalPosition = new Vector3(startPosition.x + 0.95f, startPosition.y, startPosition.z);
        explaneText.text = "¿­±â";
    }
    private void OnMouseDown()
    {
        
    }

    private void OnMouseEnter()
    {
        isEnter = true;
    }
    public void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }

    private void Update()
    {
        if(isEnter)
        {
            if (isCheck)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DrawerOpenAndClose();
                }
            }
        }
    }

    public void DrawerOpenAndClose()
    {
        if (this.gameObject.transform.position.x == startPosition.x)
        {
            this.gameObject.transform.DOMove(new Vector3(goalPosition.x, goalPosition.y, goalPosition.z), 1f, false).SetEase(Ease.InQuad);
            isEnd = true;
        }
        else if (this.gameObject.transform.position.x == goalPosition.x)
        {
            this.gameObject.transform.DOMove(new Vector3(startPosition.x, startPosition.y, startPosition.z), 1f, false).SetEase(Ease.InQuad);
            isEnd = false;
        }
        else
        {

        }
    }
}
