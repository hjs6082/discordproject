using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    public static Rotate instance;
    [SerializeField]
    private AudioSource doorOpenSound;

    public bool isEnd;
    public bool isRotateEnd;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyScript.instnace.isJoin == true)
        {
            if (isRotateEnd == false)
            {
                this.gameObject.transform.DORotate(new Vector3(-90, 0, 90), 1f);
                StartCoroutine(Kill(1));
            }
        }
        
    }

    IEnumerator Kill(float second)
    {
        yield return new WaitForSeconds(second);
        this.gameObject.transform.DOKill();
        isEnd = true;
        isRotateEnd = true;
    }
}
