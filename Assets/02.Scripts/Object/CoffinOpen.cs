using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoffinOpen : MonoBehaviour
{
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

isKey = true; // �����
        openSound = this.gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown() //���谡 ���� ��쿡 �����̰� �Ұ�. �׽�Ʈ���̶� ���콺�ٿ����� �������
    {
        if (isKey == true)
        {
            Debug.Log(2);
            this.gameObject.transform.DOMove(new Vector3(goalPosition1.x,goalPosition1.y,goalPosition1.z), 2, false).SetEase(Ease.InQuad);
            //openSound.Play();
            //StartCoroutine(WaitForMute());
            StartCoroutine(WaitForSecond(2));
            isKey = false;
        }
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
