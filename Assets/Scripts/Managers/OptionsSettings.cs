using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "OptionsData", menuName = "ScriptableObjects/OptionsData", order = 1)]
public class OptionsData : ScriptableObject 
{
    public float currentMusicVolume;
    public float currentFXVolume;
}

public class OptionsSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider SoundVolumenSlider;
    public Slider FXVolumenSlider;


    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SoundVolumenSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(SoundVolumenSlider.value) * 20);
        }

        if (PlayerPrefs.HasKey("FXVolume"))
        {
            FXVolumenSlider.value = PlayerPrefs.GetFloat("FXVolume");
            audioMixer.SetFloat("FXVolume", Mathf.Log10(FXVolumenSlider.value) * 20);
        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SoundVolumenSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(SoundVolumenSlider.value) * 20);
        }

        if (PlayerPrefs.HasKey("FXVolume"))
        {
            FXVolumenSlider.value = PlayerPrefs.GetFloat("FXVolume");
            audioMixer.SetFloat("FXVolume", Mathf.Log10(FXVolumenSlider.value) * 20);
        }
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(SoundVolumenSlider.value) * 20);
    }

    public void SetFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("FXVolume", volume);
        audioMixer.SetFloat("FXVolume", Mathf.Log10(FXVolumenSlider.value) * 20);
    }
}
