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
    

    public bool isCheck = false;
    public bool isWood = false; //唱公配阜 咯何
    public bool isMatches = false; //己成 咯何

    public bool isOneClear = false;
    public bool isClear = false;
    private bool isEnter = false;
    public bool isWoodClear = false;
    public bool isMatchesClear = false;
    public bool isFire = false;
    public bool isFireClear = false;
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
                if (isMatches && isWood)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("32");
                        if (!isOneClear)
                        {
                            isOneClear = true;
                        }
                        if (!isWoodClear && !isMatchesClear)
                        {
                            var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("Wood"));
                            Inventory.instance.items.RemoveAt(itemIndex);
                            Inventory.instance.FreshSlot();
                            isWoodClear = true;
                            isWood = false;
                        }
                    }
                }
                else if (isMatches)
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
                            if (!isMatchesClear)
                            {
                                var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("Matches"));
                                Inventory.instance.items.RemoveAt(itemIndex);
                                Inventory.instance.FreshSlot();
                                isMatchesClear = true;
                                isMatches = false;
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
                            if (!isWoodClear)
                            {
                                var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("Wood"));
                                Inventory.instance.items.RemoveAt(itemIndex);
                                Inventory.instance.FreshSlot();
                                isWoodClear = true;
                                isWood = false;
                            }
                        }
                    }
                }
                else if (isClear)
                {
                    if (!isFire)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            fireObj.Play();
                            Destroy(this.gameObject.GetComponent<ObjScript>());
                            Destroy(this.gameObject.GetComponent<Outline>());
                            StartCoroutine(KeySpawn());
                            isFireClear = true;
                        }
                    }
                }
            
            }
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
        if (!isFire)
        {
            yield return new WaitForSeconds(8f);
            Destroy(fireObj);
            keyObj.SetActive(true);
            isFire = true;
        }
    }

}
