using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class SecretDoorScript : MonoBehaviour
{
    [SerializeField] private string keyName;
    public bool isCheck = false;
    public bool isKey = false;

    public Image[] SlotImages;

    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter)
        {
            if(isCheck)
            {
                KeyCheck();
                if(isKey)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        this.gameObject.tag = "Door";
                        var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("BlueKey"));
                        Inventory.instance.items.RemoveAt(itemIndex);
                        if(itemIndex == 0)
                        {
                            SlotImages[0].sprite = null;
                        }
                        else if (itemIndex == 1)
                        {
                            SlotImages[1].sprite = null;
                        }
                        else if (itemIndex == 2)
                        {
                            SlotImages[2].sprite = null;
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        isEnter = true;   
    }
    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }

    private void KeyCheck()
    {
        if (Inventory.instance.items.Count != 0)
        {
            foreach (var items in Inventory.instance.items)
            {
                if (items.itemName == keyName)
                {
                    isKey = true;
                }
            }
        }
    }

}
