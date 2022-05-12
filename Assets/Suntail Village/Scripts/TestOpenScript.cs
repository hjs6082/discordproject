using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOpenScript : MonoBehaviour
{
    public GameObject doorObj;
    public GameObject clearPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doorObj.gameObject.GetComponent<SecretDoorScript>().isdoorOpen)
        {
            StartCoroutine(Clear());
        }
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(1.5f);
        clearPanel.SetActive(true);
    }
}
