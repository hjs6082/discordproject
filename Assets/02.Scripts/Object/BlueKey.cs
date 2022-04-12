/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueKey : MonoBehaviour
{
    [SerializeField]
    private Sprite blueKeyImage;



    private void OnMouseDown()
    {
        Destroy(this.gameObject);
        Inventory.instance.PickUp(blueKeyImage, Inventory.instance.isBlueKeyOne, Inventory.instance.isBlueKeyTwo, "블루 키", 1);
        if (Inventory.instance.inventoryOne.sprite == null && Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryOne.sprite = blueKeyImage ;
            Inventory.instance.selectOneText.text = "블루 키";
            Inventory.instance.isBlueKeyOne = true;
        }
        else if (Inventory.instance.inventoryOne.sprite != null && Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryTwo.sprite = blueKeyImage;
            Inventory.instance.isBlueKeyTwo = true;
            Inventory.instance.selectTwoText.text = "블루 키";
        }
        else if (Inventory.instance.inventoryOne.sprite == null && Inventory.instance.inventoryTwo.sprite != null)
        {
            Inventory.instance.inventoryOne.sprite = blueKeyImage;
            Inventory.instance.selectOneText.text = "블루 키";
            Inventory.instance.isBlueKeyOne = true;
        }
        else
        {
            Debug.Log("이미 인벤토리창이 꽉찼습니다.");
        }
    }
}
*/