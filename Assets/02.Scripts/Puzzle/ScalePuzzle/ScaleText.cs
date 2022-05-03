using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleText : MonoBehaviour
{
    public bool isEnter;
    [SerializeField] private GameObject GreenApple;
    public Transform spawnTrm;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
    }

    // Update is called once per frame
    void Update()
    {

/*        if (isEnter)
        {*/
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObject greenApple = Instantiate(GreenApple, spawnTrm.position, Quaternion.identity, this.gameObject.transform) as GameObject;
                greenApple.GetComponent<Rigidbody>().freezeRotation = true;
                greenApple.GetComponent<Transform>().localScale = new Vector3(1, 17.687f, 1);
                greenApple.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                greenApple.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                greenApple.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                greenApple.GetComponent<Rigidbody>().useGravity = false;
            if (Inventory.instance.items.Count > 0)
                {
                    {
                        var itemIndex = Inventory.instance.items.FindIndex(items => items.itemName.Contains("GreenApple"));
                        Inventory.instance.items.RemoveAt(itemIndex);
                        Inventory.instance.FreshSlot();


                    }
                }
            }
/*        }*/
    }
}
