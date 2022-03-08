using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{
    public GameObject[] interactionObjs; //���簡�� ������Ʈ��
    public GameObject[] notinteractobjs; //���� �Ұ� ������Ʈ��

    public bool isScan = false;

    [SerializeField]
    private Material interactionMaterial; // ���� �Ұ����� ��� �ٲ� ���׸���
    [SerializeField]
    private Material notinteractMaterial; // ���簡���� ��� �ٲ� ���׸���

    public Material[] interactionObjsMaterial; // ���� ������ �͵鿡 ������ ���׸���
    public Material[] notinteractobjsMaterial; // ���� �Ұ����� �͵鿡 ������ ���׸���

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
