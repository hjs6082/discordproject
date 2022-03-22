using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Image selectImage1;
    public Image selectImage2;

    public Image inventoryOne;
    public Image inventoryTwo;

    public bool isSelect = false;

    public bool isRedKey;
    public bool isCoffinKeyOne;
    public bool isCoffinKeyTwo;
    public bool isBlueKeyOne;
    public bool isBlueKeyTwo;
    public bool isGreenAppleOne;
    public bool isGreenAppleTwo;

    public bool isSelectOne;
    public bool isSelectTwo;

    void Start()
    {
        ClearSelect();
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            if (isSelect == false)
            {
                ClearSelect();
                selectImage1.enabled = true;
                isSelect = true;
                if(inventoryOne.sprite != null)
                {
                    ClearCursor();
                    Cursor.SetCursor(textureFromSprite(inventoryOne.sprite), Vector2.zero, CursorMode.ForceSoftware);
                }
                isSelectTwo = false;
                isSelectOne = true;
            }
            else if(selectImage1.enabled == false || selectImage2.enabled == true)
            {
                ClearSelect();
                isSelect = true;
                selectImage1.enabled = true;
                if (inventoryOne.sprite != null)
                {
                    ClearCursor();
                    Cursor.SetCursor(textureFromSprite(inventoryOne.sprite), Vector2.zero, CursorMode.ForceSoftware);
                }
                isSelectTwo = false;
                isSelectOne = true;
            }
            else
            {
                selectImage1.enabled = false;
                isSelect = false;
                if (inventoryOne.sprite != null)
                {
                    ClearCursor();
                    Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
                }
                isSelectTwo = false;
                isSelectOne = false;
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (isSelect == false)
            {
                ClearSelect();
                selectImage2.enabled = true;
                isSelect = true;
                if (inventoryTwo.sprite != null)
                {
                    ClearCursor();
                    Cursor.SetCursor(textureFromSprite(inventoryTwo.sprite), Vector2.zero, CursorMode.ForceSoftware);
                }
                isSelectOne = false;
                isSelectTwo = true;
            }
            else if (selectImage1.enabled == true || selectImage2.enabled == false)
            {
                ClearSelect();
                isSelect = true;
                selectImage2.enabled = true;
                if (inventoryTwo.sprite != null)
                {
                    ClearCursor();
                    Cursor.SetCursor(textureFromSprite(inventoryTwo.sprite), Vector2.zero, CursorMode.ForceSoftware);
                }
                isSelectOne = false;
                isSelectTwo = true;
            }
            else
            {
                selectImage2.enabled = false;
                isSelect = false;
                if (inventoryTwo.sprite != null)
                {
                    Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
                }
                isSelectOne = false;
                isSelectTwo = false;

            }


        }
    }

    void ClearSelect()
    {
        selectImage1.enabled = false;
        selectImage2.enabled = false;
    }

    void ClearCursor()
    {
        Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                        (int)sprite.textureRect.y,
                                                        (int)sprite.textureRect.width,
                                                        (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    public void PickUp(Sprite joinSprite, bool isItem1, bool isItem2)
    {
        if (Inventory.instance.inventoryOne.sprite == null && Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryOne.sprite = joinSprite;
            isItem1 = true;
        }
        else if (Inventory.instance.inventoryOne.sprite != null && Inventory.instance.inventoryTwo.sprite == null)
        {
            Inventory.instance.inventoryTwo.sprite = joinSprite;
            isItem2 = true;
        }
        else if (Inventory.instance.inventoryOne.sprite == null && Inventory.instance.inventoryTwo.sprite != null)
        {
            Inventory.instance.inventoryOne.sprite = joinSprite;
            isItem2 = true;
        }
        else
        {
            Debug.Log("이미 인벤토리창이 꽉찼습니다.");
        }
    }
}
