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
    // Start is called before the first frame update
    void Start()
    {
        searchVisionOverlay.gameObject.SetActive(false);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("n"))
        {
            if (searchVisionActive == false)
            {
                pPVol.profile = searchVision;
                searchVisionActive = true;
                searchVisionOverlay.gameObject.SetActive(true);
                Cursor.SetCursor(searchCursorImg, Vector2.zero, CursorMode.ForceSoftware);
            }
            else
            {
                pPVol.profile = standard;
                searchVisionActive = false;
                searchVisionOverlay.gameObject.SetActive(false);
                Cursor.SetCursor(standardCursorImg, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }
}
