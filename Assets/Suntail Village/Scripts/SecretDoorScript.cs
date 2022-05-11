using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class SecretDoorScript : MonoBehaviour
{
    public enum KeyType
    {
        none,
        redKey,
        blueKey,
        blackKey
    }
    public KeyType keyType = KeyType.none;
    [SerializeField] private string keyName;

    public string keyText;
    public bool isCheck = false;
    public bool isKey = false;
    private bool isClear = false;

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
                        if (!isClear)
                        {
                            switch (keyType)
                            {
                                case KeyType.none:
                                    break;
                                case KeyType.redKey:
                                    this.gameObject.tag = "Chest";
                                    break;
                                case KeyType.blueKey:
                                    this.gameObject.tag = "Door";
                                    break;
                                case KeyType.blackKey:
                                    this.gameObject.tag = "Chest";
                                    break;
                                default:
                                    break;
                            }
                            var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains(keyName));
                            Inventory.instance.items.RemoveAt(itemIndex);
                            Inventory.instance.FreshSlot();
                            isClear = true;
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
