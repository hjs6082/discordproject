using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cupboard : MonoBehaviour
{
    private bool isEnter = false;
    public bool isEnd = false;

    public bool isCheck = false;

    [SerializeField]
    private Vector3 startPosition;
    private Vector3 endPosition;

    public float backfloat;

    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y, 0.78f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter)
        {
            if (isCheck)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CupboardOpenAndClose();
                }
            }
        }
    }

    public void CupboardOpenAndClose()
    {
        if (!isEnd)
        {
            this.gameObject.transform.DOLocalMoveZ(endPosition.z, 1f);
            isEnd = true;
        }
        else if (isEnd)
        {
            this.gameObject.transform.DOLocalMoveZ(backfloat, 1f); 
            isEnd = false;
        }
        else
        {
            Debug.Log("Not");
        }
    }
}
