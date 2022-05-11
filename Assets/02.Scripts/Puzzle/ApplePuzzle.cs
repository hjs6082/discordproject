using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePuzzle : MonoBehaviour
{
    public GameObject[] greenApples;
    public bool isEnter;
    public bool isCheck;
    public bool isApple;
    public int appleCount;

    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }
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
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AppleCheck();
                    if (isApple)
                    {
                        if (greenApples != null)
                        {
                            if (!greenApples[0].activeSelf)
                            {
                                AppleDelete();
                                greenApples[0].SetActive(true);
                                appleCount++;
                            }
                            else if (greenApples[0].activeSelf)
                            {
                                AppleDelete();
                                greenApples[1].SetActive(true);
                                appleCount++;
                            }
                            else if (greenApples[0].activeSelf && greenApples[1].activeSelf)
                            {
                                AppleDelete();
                                greenApples[2].SetActive(true);
                                appleCount++;
                            }
                            isApple = false;
                        }
                    }
                }
            }
        }
    }

    private void AppleCheck()
    {
        if (Inventory.instance.items.Count != 0)
        {
            foreach (var items in Inventory.instance.items)
            {
                if (items.itemName == "GreenApple")
                {
                    isApple = true;
                }
            }
        }
    }

    private void AppleDelete()
    {
        if (Inventory.instance.items.Count != 0)
        {
            var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("GreenApple"));
            Inventory.instance.items.RemoveAt(itemIndex);
            Inventory.instance.FreshSlot();
        }
    }
}
