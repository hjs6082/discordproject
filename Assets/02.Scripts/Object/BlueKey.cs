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
        Inventory.instance.PickUp(blueKeyImage, Inventory.instance.isBlueKeyOne, Inventory.instance.isBlueKeyTwo, "��� Ű", 1);
        if (Inventory.instance.inventoryOne.sprite == null && Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryOne.sprite = blueKeyImage ;
            Inventory.instance.selectOneText.text = "��� Ű";
            Inventory.instance.isBlueKeyOne = true;
        }
        else if (Inventory.instance.inventoryOne.sprite != null && Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryTwo.sprite = blueKeyImage;
            Inventory.instance.isBlueKeyTwo = true;
            Inventory.instance.selectTwoText.text = "��� Ű";
        }
        else if (Inventory.instance.inventoryOne.sprite == null && Inventory.instance.inventoryTwo.sprite != null)
        {
            Inventory.instance.inventoryOne.sprite = blueKeyImage;
            Inventory.instance.selectOneText.text = "��� Ű";
            Inventory.instance.isBlueKeyOne = true;
        }
        else
        {
            Debug.Log("�̹� �κ��丮â�� ��á���ϴ�.");
        }
    }
}
*/