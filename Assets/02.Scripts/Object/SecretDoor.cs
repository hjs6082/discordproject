using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecretDoor : MonoBehaviour
{
    public static SecretDoor instnace;
    public GameObject doorKey;

    private Vector3 startPosition;
    private Vector3 endPosition;

    public bool isKeyEnd;
    public bool isRotateEnd;

    public bool isDoorOpen;

    private Vector3 offset;

    private float zOffset;

    private Vector3 nowOffset;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = doorKey.gameObject.transform.position;
        instnace = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isKeyEnd == true)
        {
            Debug.Log("Play");
        }
    }
    private void OnMouseDrag()
    {
        if (isDoorOpen)
        {
            transform.position = GetMouseWorldPos() + offset;
            nowOffset = transform.position;

            this.gameObject.transform.position = new Vector3(nowOffset.x, nowOffset.y + 0.3f, nowOffset.z);
        }
    }

    private void OnMouseDown()
    {
/*        Debug.Log("#@$");
        if (Inventory.instance.isSelectOne && Inventory.instance.isBlueKeyOne || Inventory.instance.isSelectTwo && Inventory.instance.isBlueKeyTwo)
        {*/
            Debug.Log("234");
            doorKey.SetActive(true);
            doorKey.transform.DOMove(new Vector3(startPosition.x, startPosition.y- 1.57f, startPosition.z),1.5f, false);
            StartCoroutine(EndKey());
            if (isDoorOpen)
            {
                zOffset = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

                offset = gameObject.transform.position - GetMouseWorldPos();
            }
        //}
    }

    public IEnumerator EndKey()
    {
        yield return new WaitForSeconds(1.5f);
        DOTween.Clear();
        isKeyEnd = true;
    }

    public IEnumerator EndRotate()
    {
        StopCoroutine(EndKey());
        yield return new WaitForSeconds(1f);
        DOTween.Clear();
        isRotateEnd = true;
        isKeyEnd = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
