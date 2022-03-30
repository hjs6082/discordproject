using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorRotate : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SecretDoor.instnace.isRotateEnd)
        {
            StopCoroutine(SecretDoor.instnace.EndRotate());
            //this.gameObject.transform.DORotate(new Vector3(0, 0, 0), 1f);
        }
    }
}
