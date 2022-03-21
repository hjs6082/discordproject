using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossKey : MonoBehaviour
{
    [SerializeField]
    private Sprite crossImage;
    
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Destroy(this.gameObject);
        if(Inventory.instance.inventoryOne.sprite == null || Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryOne.sprite = crossImage;
        }
        else if (Inventory.instance.inventoryOne.sprite != null || Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryTwo.sprite = crossImage;
        }
        else if (Inventory.instance.inventoryOne.sprite == null || Inventory.instance.inventoryTwo.sprite != null)
        {
            Inventory.instance.inventoryOne.sprite = crossImage;
        }   
        else
        {
            Debug.Log("이미 인벤토리창이 꽉찼습니다.");
        }


    }
}
