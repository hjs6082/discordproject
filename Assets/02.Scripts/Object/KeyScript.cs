using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeyScript : MonoBehaviour
{
    public static KeyScript instnace;
    private Vector3 startPosition;
    private Vector3 goalPosition;
    private Vector3 backPosition;

    private bool isEnd;
    private bool isMoveEnd;
    public bool isJoin = false;
    // Start is called before the first frame update
    void Start()
    {
        instnace = this;
        startPosition = this.gameObject.transform.position;
        goalPosition = new Vector3(startPosition.x + 3f, startPosition.y, startPosition.z);
        backPosition = new Vector3(goalPosition.x - 1f, goalPosition.y, goalPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        //�κ��丮���� ���踦 ������ Ŭ���ϸ� ������ҿ��� J�� ���￹��.
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isMoveEnd == false)
            {
                this.gameObject.transform.DOMove(goalPosition, 1.5f, false).SetEase(Ease.InQuad);
                StartCoroutine(Kill(1.5f));
                StartCoroutine(EndMove(1.5f));
            }

        }
        if (this.gameObject.transform.position == goalPosition)
        {
            isJoin = true;
        }
    }

    private void OnMouseDown()
    {
        if(Rotate.instance.isEnd)
        {
            Debug.Log("Yes");
            this.gameObject.transform.DOMove(backPosition, 0.4f, false);
            StartCoroutine(Kill(0.4f));
            StartCoroutine(End(0.4f));
        }
        if(isEnd == true)
        {
            Debug.Log("End");
            Destroy(gameObject);

            //�κ��丮�� �������� �ٲܰ���
        }
    }

    IEnumerator Kill(float second)
    {
        yield return new WaitForSeconds(second);
        this.gameObject.transform.DOKill();
    }

    IEnumerator End(float second)
    {
        yield return new WaitForSeconds(second);
        isEnd = true;
    }
    IEnumerator EndMove(float second)
    {
        yield return new WaitForSeconds(second);
        isMoveEnd = true;
    }
}
