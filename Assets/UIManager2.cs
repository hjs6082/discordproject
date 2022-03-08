using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{
    public GameObject[] interactionObjs; //조사가능 오브젝트들
    public GameObject[] notinteractobjs; //조사 불가 오브젝트들

    public bool isScan = false;

    [SerializeField]
    private Material interactionMaterial; // 조사 불가능할 경우 바뀔 메테리얼
    [SerializeField]
    private Material notinteractMaterial; // 조사가능할 경우 바뀔 메테리얼

    public Material[] interactionObjsMaterial; // 조사 가능한 것들에 저장할 메테리얼
    public Material[] notinteractobjsMaterial; // 조사 불가능한 것들에 저장한 메테리얼

    void Start()
    {
        for(int i = 0; i <= interactionObjs.Length; i++)
        {
            interactionObjsMaterial[i] = interactionObjs[i].GetComponent<MeshRenderer>().material;
        }
        for(int i = 0; i <= notinteractobjs.Length; i++)
        {
            notinteractobjsMaterial[i] = notinteractobjs[i].GetComponent<MeshRenderer>().material;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            if (isScan == false)
            {
               for (int i = 0; i == interactionObjs.Length; i++)
               {
                    interactionObjs[i].GetComponent<MeshRenderer>().material = interactionMaterial;
                }
                for (int i = 0; i == notinteractobjs.Length; i++)
                {
                    notinteractobjs[i].GetComponent<MeshRenderer>().material = notinteractMaterial;
                }
                isScan = true;



            }
            else
            {
                for (int i = 0; i == interactionObjs.Length; i++)
                {
                    interactionObjs[i].GetComponent<MeshRenderer>().material = interactionObjsMaterial[i];
                    isScan = false;
                }
                for (int i = 0; i == notinteractobjs.Length; i++)
                {
                    notinteractobjs[i].GetComponent<MeshRenderer>().material = notinteractobjsMaterial[i];
                }
           }
        }
    }

}
