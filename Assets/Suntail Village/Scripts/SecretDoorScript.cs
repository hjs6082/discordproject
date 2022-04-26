using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SecretDoorScript : MonoBehaviour
{
    [SerializeField] private string keyName;
    public bool isCheck = false;
    public bool isKey = false;

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
        foreach (var items in Inventory.instance.items)
        {
            if(items.itemName == keyName)
            {
                isKey = true;
                int itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("BlueKey"));
                Inventory.instance.items.RemoveAt(itemIndex);
            }
        }

    }

}
