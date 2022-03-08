using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject[] objs;
    public Material[] objMaterials;

    [SerializeField]
    private Material notInterativeObj;
    public bool isScan = false;

    void Start()
    {
        for (int i = 0; i <= objs.Length; i++)
        {
            objMaterials[i] = objs[i].GetComponent<MeshRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("t"))
        {
            if (isScan == false)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    objs[i].GetComponent<MeshRenderer>().material = notInterativeObj;
                    isScan = true;
                }
            }
            else
            {
                for(int i = 0; i < objs.Length; i++)
                {
                    objs[i].GetComponent<MeshRenderer>().material = objMaterials[i];
                    isScan = false;
                }
            }
        }
    }
}
