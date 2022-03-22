using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoffinOpen : MonoBehaviour
{
    public GameObject crossKey;
    private Vector3 crossKeyStartPosition;



    private Vector3 startPosition;
    private bool isKey = false;


    private Vector3 goalPosition1;

    [SerializeField]
    private AudioSource openSound;
 
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        goalPosition1 = new Vector3(startPosition.x, startPosition.y + 0.054f, startPosition.z);
        openSound = this.gameObject.GetComponent<AudioSource>();
        crossKeyStartPosition = crossKey.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKey == true)
        {
            StopCoroutine(WaitForKey());
            Debug.Log(2);
            this.gameObject.transform.DOMove(new Vector3(goalPosition1.x, goalPosition1.y, goalPosition1.z), 2, false).SetEase(Ease.InQuad);
            //openSound.Play();
            //StartCoroutine(WaitForMute());


            StartCoroutine(WaitForSecond(2));
            isKey = false;

        }
    }

    private void OnMouseDown() //열쇠가 꽃힐 경우에 움직이게 할것. 테스트용이라서 마우스다운으로 맞춰놓음
    {
        if (Inventory.instance.isSelectOne || Inventory.instance.isCoffinKeyOne && Inventory.instance.isSelectTwo && Inventory.instance.isCoffinKeyTwo)
        {
            Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
            Inventory.instance.isSelect = false;
            if (Inventory.instance.isSelectOne || Inventory.instance.isCoffinKeyOne)
            {
                Inventory.instance.isSelectOne = false;
                Inventory.instance.isCoffinKeyOne = false;
                Inventory.instance.inventoryOne.sprite = null;
                Inventory.instance.selectImage1.enabled = false;
            }
            else if (Inventory.instance.isSelectTwo || Inventory.instance.isCoffinKeyTwo)
            {
                Inventory.instance.isSelectTwo = false;
                Inventory.instance.isCoffinKeyTwo = false;
                Inventory.instance.inventoryTwo.sprite = null;
                Inventory.instance.selectImage2.enabled = false;
            }
            crossKey.transform.DOMove(new Vector3(crossKeyStartPosition.x, 1.5f, crossKeyStartPosition.z), 2f, false).SetEase(Ease.InQuad);
            StartCoroutine(WaitForKey());
        }
        else
        {
            Debug.Log("열쇠가 없습니다.");
        }
    }


    IEnumerator WaitForKey()
    {
        yield return new WaitForSeconds(2);
        isKey = true;
    }

    IEnumerator WaitForMute()
    {
        yield return new WaitForSeconds(2);
        openSound.mute = true;
        yield return new WaitForSeconds(2);
    }

    IEnumerator WaitForSecond(int second)
    {
        yield return new WaitForSeconds(second);
        this.gameObject.transform.DOMove(new Vector3(startPosition.x, startPosition.y, startPosition.z + 1.2f), 2, false).SetEase(Ease.InQuad);
        openSound.Play();
        openSound.mute = false;
        yield return new WaitForSeconds(second);
        openSound.mute = true;
    }
}
