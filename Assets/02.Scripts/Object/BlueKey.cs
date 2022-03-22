using System.Collections;
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
        Inventory.instance.PickUp(blueKeyImage, Inventory.instance.isBlueKeyOne, Inventory.instance.isBlueKeyTwo);
    }
}
