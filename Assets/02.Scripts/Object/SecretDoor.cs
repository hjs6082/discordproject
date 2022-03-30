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
            doorKey.gameObject.transform.DORotate(new Vector3(0, -90, 0), 1f);
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
}
