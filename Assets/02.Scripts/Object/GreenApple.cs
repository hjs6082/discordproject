using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenApple : MonoBehaviour
{
    [SerializeField]
    private Sprite greenAppleImage;

    private void OnMouseDown()
    {
        Destroy(this.gameObject);
        Inventory.instance.PickUp(greenAppleImage, Inventory.instance.isGreenAppleOne, Inventory.instance.isGreenAppleTwo,"초록 사과", 3);
    }
}
