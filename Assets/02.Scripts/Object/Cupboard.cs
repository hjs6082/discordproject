using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cupboard : MonoBehaviour
{
    private bool isEnter = false;
    public bool isEnd = false;

    [SerializeField]
    private Vector3 startPosition;
    private Vector3 endPosition;


    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
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
            if(Input.GetKeyDown(KeyCode.E))
            {
                CupboardOpenAndClose();
            }
        }
    }

    public void CupboardOpenAndClose()
    {
        if (!isEnd)
        {
            this.gameObject.GetComponent<Transform>().DOLocalMoveZ(endPosition.z, 1f);
            isEnd = true;
        }
        else if (isEnd)
        {
            this.gameObject.GetComponent<Transform>().DOLocalMoveZ(0.3322172f, 1f); 
            isEnd = false;
        }
        else
        {
            Debug.Log("Not");
        }
    }
}
