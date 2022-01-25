using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    PostProcessVolume pPVol;
    [SerializeField]
    PostProcessProfile standard;
    [SerializeField]
    PostProcessProfile searchVision;
    [SerializeField]
    GameObject searchVisionOverlay;
    [SerializeField]
    Texture2D searchCursorImg;
    [SerializeField]
    Texture2D standardCursorImg;

    public bool searchVisionActive = false;
    public bool canSearchVisionActive = false;

    public GameObject[] interactionObjs;
    public GameObject[] notinteractobjs;
    public GameObject MovePuzzle;

    public GameObject searchModeUI;

    [SerializeField]
    Material interactMaterial;
    [SerializeField]
    Material standardMaterial;
    [SerializeField]
    Material returnMaterial;
    [SerializeField]
    Material deskMaterial;

    void Start()
    {
        searchVisionOverlay.gameObject.SetActive(false);
        instance = this;
    }


    void Update()
    {
        //조사모드 돌입
        if (Input.GetKeyDown("n"))
        {
            if (searchVisionActive == false)
            {
                searchVisionOverlay.gameObject.SetActive(true);
                Cursor.SetCursor(searchCursorImg, Vector2.zero, CursorMode.ForceSoftware);
                searchVisionActive = true;
            }
            else
            {
                searchVisionOverlay.gameObject.SetActive(false);
                Cursor.SetCursor(standardCursorImg, Vector2.zero, CursorMode.ForceSoftware);
                searchVisionActive = false;
            }
        }
        //조사가능 대상찾기
        if(Input.GetKeyDown("e"))
        {
            if (canSearchVisionActive == false)
            {
                pPVol.profile = searchVision;
                canSearchVisionActive = true;
                for(int i = 0; i < interactionObjs.Length; i++)
                {
                    interactionObjs[i].GetComponent<MeshRenderer>().material = interactMaterial;
                }
                for (int i = 0; i < notinteractobjs.Length; i++)
                {
                    notinteractobjs[i].GetComponent<MeshRenderer>().material = standardMaterial;
                }
                MovePuzzle.GetComponent<MeshRenderer>().material = interactMaterial;
            }
            else
            {
                pPVol.profile = standard;
                canSearchVisionActive = false;
                for (int i = 0; i < notinteractobjs.Length; i++)
                {
                    notinteractobjs[i].GetComponent<MeshRenderer>().material = returnMaterial;
                }
                for (int i = 0; i < interactionObjs.Length; i++)
                {
                    interactionObjs[i].GetComponent<MeshRenderer>().material = returnMaterial;
                }
                MovePuzzle.GetComponent<MeshRenderer>().material = deskMaterial;
            }
        }
    }

    public void ResetCursor()
    {
        searchVisionOverlay.gameObject.SetActive(false);
        Cursor.SetCursor(standardCursorImg, Vector2.zero, CursorMode.ForceSoftware);
        searchVisionActive = false;
    }
    
}
