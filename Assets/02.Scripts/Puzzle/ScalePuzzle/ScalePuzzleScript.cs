using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePuzzleScript : MonoBehaviour
{
    private bool isEnter = false;
    public int bookCount = 0;
    public static bool scalePuzzleClear = false;

    [SerializeField]
    private Suntail.PlayerInteractions pi;
    
    
    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
    }

    private void Update()
    {
        if(isEnter)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                pi.BreakConnection();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DropObject(collision.gameObject);
    }

    public void DropObject(GameObject gmObj)
    {
        if(gmObj.tag == "Item")
        {
            if(gmObj.name.Contains("Book"))
            {
                gmObj.tag = "Untagged";
                bookCount++;
                Debug.Log(bookCount);
            }
        }
    }
}
