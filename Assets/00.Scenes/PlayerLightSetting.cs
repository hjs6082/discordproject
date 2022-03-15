using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerLightSetting : MonoBehaviour
{
    [SerializeField]
    PostProcessVolume PPVol;
    [SerializeField]
    PostProcessProfile Standard;
    [SerializeField]
    PostProcessProfile NightVision;
    [SerializeField]
    GameObject NightVisionOverlay;
    [SerializeField]
    GameObject FlashLight;

    private bool NightVisionActive = false;
    private bool FlashLightActive = false;
    // Start is called before the first frame update
    void Start()
    {
        NightVisionOverlay.gameObject.SetActive(false);
        FlashLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("n"))
        {
            if (NightVisionActive == false)
            {
                PPVol.profile = NightVision;
                NightVisionActive = true;
                NightVisionOverlay.gameObject.SetActive(true);
            }
            else
            {
                PPVol.profile = Standard;
                NightVisionActive = false;
                NightVisionOverlay.gameObject.SetActive(false);
            }
        }
        if(Input.GetKeyDown("f"))
        {
            if(FlashLightActive == false)
            {
                FlashLightActive = true;
                FlashLight.gameObject.SetActive(true);
            }
            else
            {
                FlashLightActive = false;
                FlashLight.gameObject.SetActive(false);
            }
        }
    }
}
