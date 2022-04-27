using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordPaper : MonoBehaviour
{
    private bool isEnter = false;
    public bool isCheck = false;
    public GameObject player;

    [SerializeField] private GameObject paperPanel;

    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter)
        {
            if(isCheck)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    paperPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    player.GetComponent<Suntail.PlayerController>().enabled = false;
                }
            }
        }
    }
}
