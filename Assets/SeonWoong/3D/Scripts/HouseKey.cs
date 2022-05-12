using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseKey : MonoBehaviour
{
    private MyData myData = null;

    private void Awake()
    {
        myData = GetComponent<MyData>();
    }

    private void OnMouseDown()
    {
        Inventory.instance.AddItem(myData.myData);    
        
        this.gameObject.SetActive(false);
    }
}
