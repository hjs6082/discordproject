using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorTween : MonoBehaviour
{
    public GameObject door;
    public static DoorTween instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void DoorMove()
    {
        door.transform.DOLocalRotate(new Vector3(-90, 0, 90), 2.5f);
    }
}
