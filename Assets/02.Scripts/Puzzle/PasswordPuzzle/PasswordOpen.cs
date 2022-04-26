using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PasswordOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject passwordPanel;
    [SerializeField]
    private GameObject player;
    
    public bool puzzleClear;
    public bool isCheck;
    private bool isEnter;
    private bool isPuzzleUp;

    private Vector3 passwordPanelPosition;
    // Start is called before the first frame update
    void Start()
    {
        passwordPanelPosition = passwordPanel.GetComponent<RectTransform>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            if (isCheck)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PasswordUp();
                }
            }
        }

        if(!puzzleClear)
        {
            if (isPuzzleUp)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PasswordDown();
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        isEnter = true;
    }
    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }


    public void PasswordUp()
    {
        if (!puzzleClear)
        {
            passwordPanel.GetComponent<RectTransform>().transform.DOLocalMoveY(0f, 1.5f, false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<Suntail.PlayerController>().enabled = false;
            isPuzzleUp = true;
        }
    }
    public void PasswordDown()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        passwordPanel.GetComponent<RectTransform>().transform.DOLocalMoveY(-1008f, 1f, false);
        player.GetComponent<Suntail.PlayerController>().enabled = true;
        isPuzzleUp = false;
    }
}
