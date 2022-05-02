using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePuzzle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireObj;
    [SerializeField]
    private GameObject keyObj;
    [SerializeField]
    private string matchesName = "Matches";
    [SerializeField]
    private string woodName = "Wood";
    
    public Image[] SlotImages;

    public bool isCheck = false;
    public bool isWood = false; //唱公配阜 咯何
    public bool isMatches = false; //己成 咯何

    public bool isOneClear = false;
    public bool isClear = false;
    private bool isEnter = false;
    // Start is called before the first frame update

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
        fireObj.Stop();
    }
    void Update()
    {
        if (isEnter)
        {
            if (isCheck)
            {
                isMatchesCheck();
                isWoodCheck();
                if (isMatches)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if(!isOneClear)
                        {
                            isOneClear = true;
                        }
                        else if(isOneClear)
                        {
                            isClear = true;
                        }
                        if (Inventory.instance.items.Count > 0)
                        {
                            var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("Matches"));
                            Inventory.instance.items.RemoveAt(itemIndex);
                            if (itemIndex == 0)
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
                        }
                    }
                }
                else if (isWood)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!isOneClear)
                        {
                            isOneClear = true;
                        }
                        else if (isOneClear)
                        {
                            isClear = true;
                        }
                        if (Inventory.instance.items.Count > 0)
                        {
                            var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("Wood"));
                            Inventory.instance.items.RemoveAt(itemIndex);
                            if (itemIndex == 0)
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
                        }
                    }
                }
                else if (isMatches && isWood)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!isOneClear)
                        {
                            isOneClear = true;
                        }
                        else if (isOneClear)
                        {
                            isClear = true;
                        }
                        var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("Wood"));
                        Inventory.instance.items.RemoveAt(itemIndex);
                        if (itemIndex == 0)
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
                    }
                }
            }
        }
        else if (isClear)
        {
            //if (Input.GetKeyDown(KeyCode.E))
            //{
                fireObj.Play();
                Destroy(this.gameObject.GetComponent<ObjScript>());
                Destroy(this.gameObject.GetComponent<Outline>());
                StartCoroutine(KeySpawn());
            //}
        }
    }

    public void isMatchesCheck()
    {
        if (Inventory.instance.items.Count != 0)
        {
            foreach (var items in Inventory.instance.items)
            {
                if (items.itemName == matchesName)
                {
                    isMatches = true;
                }
            }
        }
    }

    public void isWoodCheck()
    {
        if (Inventory.instance.items.Count != 0)
        {
            foreach (var items in Inventory.instance.items)
            {
                if (items.itemName == woodName)
                {
                    isWood = true;
                }
            }
        }
    }

    IEnumerator KeySpawn()
    {
        yield return new WaitForSeconds(2f);
        Destroy(fireObj);
        keyObj.SetActive(true);
    }

}
