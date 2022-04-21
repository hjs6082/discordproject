using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PasswordTest : MonoBehaviour
{
    [SerializeField]
    private GameObject passwordPanel;

    private Vector3 passwordPanelPosition;
    // Start is called before the first frame update
    void Start()
    {
        passwordPanelPosition = passwordPanel.GetComponent<RectTransform>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        PasswordUp();
    }

    public void PasswordUp()
    {
        passwordPanel.GetComponent<RectTransform>().transform.DOLocalMoveY(0f,1.5f,false);
    }
    public void PasswordDown()
    {
        passwordPanel.GetComponent<RectTransform>().transform.DOLocalMoveY(-1008f, 1f, false);
    }
}
