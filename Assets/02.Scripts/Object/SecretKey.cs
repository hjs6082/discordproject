using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecretKey : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField]

    public bool rotateEnd;

    private void OnEnable()
    {
        StartCoroutine(WaitForRotate());
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateEnd)
        {
            StopCoroutine(WaitForRotate());
            this.gameObject.transform.DOMove(new Vector3(startPosition.x, startPosition.y, startPosition.z), 1.5f, false);
            SecretDoor.instnace.isDoorOpen = true;
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForRotate()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.transform.DORotate(new Vector3(0, -90, 0), 1f);
        yield return new WaitForSeconds(3f);
        DOTween.Clear();
        yield return new WaitForSeconds(1f);
        rotateEnd = true;
    }
}
