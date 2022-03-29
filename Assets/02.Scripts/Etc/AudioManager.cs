using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip buttonEffect;
    public AudioClip teleportEffect;
    public AudioClip moveEffect;
    public AudioClip fairyEffect;
    public AudioClip clearEffect;
    public AudioClip[] attackEffect;

    public AudioSource BGM_Source;
    public GameObject BGM_Volume;
    private Slider BGM_Slider;
    private Toggle BGM_Toggle;

    public AudioSource EFFECT_Source;
    public GameObject EFFECT_Volume;
    private Slider EFFECT_Slider;
    private Toggle EFFECT_Toggle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitVolumeSettings();
    }

    public void InitVolumeSettings()
    {
        EFFECT_Source = GetComponent<AudioSource>();

        BGM_Slider = BGM_Volume?.GetComponentInChildren<Slider>();
        BGM_Toggle = BGM_Volume?.GetComponentInChildren<Toggle>();

        EFFECT_Slider = EFFECT_Volume?.GetComponentInChildren<Slider>();
        EFFECT_Toggle = EFFECT_Volume?.GetComponentInChildren<Toggle>();

        BGM_Slider.value = BGM_Source.volume;
        BGM_Toggle.isOn = BGM_Source.mute;

        EFFECT_Slider.value = EFFECT_Source.volume;
        EFFECT_Toggle.isOn = EFFECT_Source.mute;
    }

    private void Update()
    {
        if (BGM_Slider.value != BGM_Source.volume) { BGM_Source.volume = BGM_Slider.value; }
        if (BGM_Toggle.isOn != BGM_Source.mute) { BGM_Source.mute = BGM_Toggle.isOn; }

        if (EFFECT_Slider.value != EFFECT_Source.volume) { EFFECT_Source.volume = EFFECT_Slider.value; }
        if (EFFECT_Toggle.isOn != EFFECT_Source.mute) { EFFECT_Source.mute = EFFECT_Toggle.isOn; }
    }

    private void ChangeSound(AudioClip clip)
    {
        if (EFFECT_Source != null)
        {
            EFFECT_Source.Stop();
            EFFECT_Source.clip = clip;
            EFFECT_Source.Play();
        }
    }

    public void ChangeBGM(AudioClip clip)
    {
        BGM_Source.Stop();
        BGM_Source.clip = clip;
        BGM_Source.Play();
    }

    public void ButtonSound()
    {
        ChangeSound(buttonEffect);
    }

    public void TeleportSound()
    {
        ChangeSound(teleportEffect);
    }

    public void MoveSound()
    {
        StartCoroutine(IEffectSound(moveEffect, 0.5f));
    }

    public void AttackSound()
    {
        int rand = Random.Range(0, attackEffect.Length);
        ChangeSound(attackEffect[rand]);
    }

    public void FairySound()
    {
        StartCoroutine(IEffectSound(fairyEffect, 1f));
    }

    public void ClearSound()
    {
        ChangeSound(clearEffect);
    }

    IEnumerator IEffectSound(AudioClip effect, float duration)
    {
        ChangeSound(effect);
        yield return new WaitForSeconds(duration);
        EFFECT_Source.Stop();
    }
}
