using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject[] objPositionList;

    private int objNumber;

    public Image selectImage1;
    public Image selectImage2;

    public Image inventoryOne;
    public Image inventoryTwo;

    private GameObject gameobj1;
    private GameObject gameobj2;

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

    public Text selectOneText;
    public Text selectTwoText;

    private int selectObj1 = 0;
    private int selectObj2 = 0;


    void Start()
    {
        ClearSelect();
        if(instance == null)
        {
            instance = this;
        }
        selectOneText.enabled = false;
        selectTwoText.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (isSelect == false)
            {
                ClearObj();
                ClearText();
                ClearSelect();
                selectImage1.enabled = true;
                selectOneText.enabled = true;
                isSelect = true;
                objPositionList[selectObj1 - 1].gameObject.SetActive(true);
                isSelectTwo = false;
                isSelectOne = true;
            }
            else if(selectImage1.enabled == false || selectImage2.enabled == true)
            {
                ClearObj();
                ClearText();
                ClearSelect();
                isSelect = true;
                selectImage1.enabled = true;
                selectOneText.enabled = true;
                objPositionList[selectObj1 - 1].gameObject.SetActive(true);
                isSelectTwo = false;
                isSelectOne = true;
            }
            else
            {
                selectImage1.enabled = false;
                selectOneText.enabled = false;
                isSelect = false;
                objPositionList[selectObj1 - 1].gameObject.SetActive(false);
                isSelectTwo = false;
                isSelectOne = false;
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (isSelect == false)
            {
                ClearObj();
                ClearText();
                ClearSelect();
                selectImage2.enabled = true;
                selectTwoText.enabled = true;
                isSelect = true;
                objPositionList[selectObj2 - 1].gameObject.SetActive(true);
                isSelectOne = false;
                isSelectTwo = true;
            }
            else if (selectImage1.enabled == true || selectImage2.enabled == false)
            {
                ClearObj();
                ClearText();
                ClearSelect();
                isSelect = true;
                selectImage2.enabled = true;
                selectTwoText.enabled = true;
                objPositionList[selectObj2 - 1].gameObject.SetActive(true);
                isSelectOne = false;
                isSelectTwo = true;
            }
            else
            {
                selectImage2.enabled = false;
                isSelect = false;
                selectTwoText.enabled = false;
                objPositionList[selectObj2 - 1].gameObject.SetActive(false);
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

    void ClearText()
    {
        selectOneText.enabled = false;
        selectTwoText.enabled = false;
    }

    void ClearObj()
    {
        for(int i = 0; i < objPositionList.Length; i++)
        {
            objPositionList[i].gameObject.SetActive(false);
        }
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

    public void PickUp(Sprite joinSprite, bool isItem1, bool isItem2, string itemName, int number)
    {
        if (inventoryOne.sprite == null && inventoryTwo.sprite == null)
        {
            inventoryOne.sprite = joinSprite;
            isItem1 = true;
            selectOneText.text = itemName;

        }
        else if (instance.inventoryOne.sprite != null && inventoryTwo.sprite == null)
        {
            inventoryTwo.sprite = joinSprite;
            isItem2 = true;
            selectTwoText.text = itemName;
        }
        else if (inventoryOne.sprite == null && inventoryTwo.sprite != null)
        {
            inventoryOne.sprite = joinSprite;
            isItem2 = true;
            selectOneText.text = itemName;
        }
        else
        {
            Debug.Log("ÀÌ¹Ì ÀÎº¥Åä¸®Ã¢ÀÌ ²ËÃ¡½À´Ï´Ù.");
        }
        if(number == 1)
        {
            if(selectObj1 == 0 && selectObj2 == 0)
            {
                selectObj1 = 1;
            }
            else if(selectObj1 != 0 && selectObj2 == 0)
            {
                selectObj2 = 1;
            }
            else if(selectObj1 == 0 && selectObj2 != 0)
            {
                selectObj1 = 1;
            }
            else
            {
                Debug.Log("ÀÎº¥Åä¸®Ã¢ÀÌ ²ËÃ¡½À´Ï´Ù");
            }
        }
        else if(number == 2 )
        {
            objNumber = 2;
            if (selectObj1 == 0 && selectObj2 == 0)
            {
                selectObj1 = 2;
            }
            else if (selectObj1 != 0 && selectObj2 == 0)
            {
                selectObj2 = 2;
            }
            else if (selectObj1 == 0 && selectObj2 != 0)
            {
                selectObj1 = 2;
            }
            else
            {
                Debug.Log("ÀÎº¥Åä¸®Ã¢ÀÌ ²ËÃ¡½À´Ï´Ù");
            }
        }
        else if(number == 3)
        {
            objNumber = 3;
            if (selectObj1 == 0 && selectObj2 == 0)
            {
                selectObj1 = 3;
            }
            else if (selectObj1 != 0 && selectObj2 == 0)
            {
                selectObj2 = 3;
            }
            else if (selectObj1 == 0 && selectObj2 != 0)
            {
                selectObj1 = 3;
            }
            else
            {
                Debug.Log("ÀÎº¥Åä¸®Ã¢ÀÌ ²ËÃ¡½À´Ï´Ù");
            }
        }
        else
        {
            Debug.Log("¾ø½À´Ï´Ù.");
        }
    }
}
