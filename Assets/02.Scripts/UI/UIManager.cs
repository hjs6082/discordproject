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
    public GameObject movePuzzle;
    public GameObject chessPuzzleBlack;
    public GameObject chessPuzzleWhite;

    public GameObject searchModeUI;
    public GameObject scanModeUI;
    public GameObject moveModeUI;

    [SerializeField]
    Material interactMaterial;
    [SerializeField]
    Material standardMaterial;
    [SerializeField]
    Material returnMaterial;
    [SerializeField]
    Material deskMaterial;
    [SerializeField]
    Material chessWhiteMaterial;
    [SerializeField]
    Material chessBlackMaterial;

    void Start()
    {
        searchVisionOverlay.gameObject.SetActive(false);
        instance = this;

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if(!GameManager.Instance.bPause)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //������ ����
        if (Input.GetKeyDown("n"))
        {
            if (searchVisionActive == false)
            {
                clearUI();
                searchVisionOverlay.gameObject.SetActive(true);
                Cursor.SetCursor(searchCursorImg, Vector2.zero, CursorMode.ForceSoftware);
                searchVisionActive = true;
            }
            else
            {
                clearUI();
                Cursor.SetCursor(standardCursorImg, Vector2.zero, CursorMode.ForceSoftware);
                searchVisionActive = false;
                moveModeUI.SetActive(true);
            }
        }
        //���簡�� ���ã��
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
                movePuzzle.GetComponent<MeshRenderer>().material = interactMaterial;
                clearUI();
                scanModeUI.SetActive(true);
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
                movePuzzle.GetComponent<MeshRenderer>().material = deskMaterial;
                chessPuzzleWhite.GetComponent<MeshRenderer>().material = chessWhiteMaterial;
                chessPuzzleBlack.GetComponent<MeshRenderer>().material = chessBlackMaterial;
                clearUI();
                moveModeUI.SetActive(true);
            }
        }
    }

    public void clearUI()
    {
        searchModeUI.SetActive(false);
        scanModeUI.SetActive(false);
        moveModeUI.SetActive(false);
    }

    public void ResetCursor()
    {
        searchVisionOverlay.gameObject.SetActive(false);
        Vector2 cursorHotspot = new Vector2 (standardCursorImg.width / 2, standardCursorImg.height / 2);
        Cursor.SetCursor(standardCursorImg, cursorHotspot, CursorMode.ForceSoftware);
        searchVisionActive = false;
    }
    
}
