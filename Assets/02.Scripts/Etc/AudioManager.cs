using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM_Source;
    public  GameObject  BGM_Volume;
    private Slider      BGM_Slider;
    private Toggle      BGM_Toggle;

    public AudioSource EFFECT_Source;
    public  GameObject  EFFECT_Volume;
    private Slider      EFFECT_Slider;
    private Toggle      EFFECT_Toggle;

    private void Awake()
    {
        EFFECT_Source = GetComponent<AudioSource>();

        BGM_Slider = BGM_Volume.GetComponentInChildren<Slider>();
        BGM_Toggle = BGM_Volume.GetComponentInChildren<Toggle>();

        EFFECT_Slider = EFFECT_Volume.GetComponentInChildren<Slider>();
        EFFECT_Toggle = EFFECT_Volume.GetComponentInChildren<Toggle>();
    }

    private void Update()
    {
        if(BGM_Slider.value != BGM_Source.volume) { BGM_Source.volume = BGM_Slider.value; }
        if(BGM_Toggle.isOn != BGM_Source.mute) { BGM_Source.mute = BGM_Toggle.isOn; }

        if(EFFECT_Slider.value != EFFECT_Source.volume) { EFFECT_Source.volume = EFFECT_Slider.value; }
        if(EFFECT_Toggle.isOn != EFFECT_Source.mute) { EFFECT_Source.mute = EFFECT_Toggle.isOn; }
    }
}
