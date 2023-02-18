using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsSettings : MonoBehaviour
{
    public Slider SoundVolumenSlider;
    public Slider FXVolumenSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SoundVolumenSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(SoundVolumenSlider.value);
        }

        if (PlayerPrefs.HasKey("FXVolume"))
        {
            FXVolumenSlider.value = PlayerPrefs.GetFloat("FXVolume");
            SetFXVolume(FXVolumenSlider.value);
        }

        if (AudioManager.Instance) 
        {
            SoundVolumenSlider.onValueChanged.AddListener(SetMusicVolume);
            FXVolumenSlider.onValueChanged.AddListener(SetFXVolume);
        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SoundVolumenSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(SoundVolumenSlider.value);
        }

        if (PlayerPrefs.HasKey("FXVolume"))
        {
            FXVolumenSlider.value = PlayerPrefs.GetFloat("FXVolume");
            SetFXVolume(FXVolumenSlider.value);
        }

        if (AudioManager.Instance)
        {
            SoundVolumenSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolumen);
            FXVolumenSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolumen);
        }
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);

        if (AudioManager.Instance) 
        {
            AudioManager.Instance.SetMusicVolumen(volume);
        }
    }

    public void SetFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("FXVolume", volume);

        if (AudioManager.Instance)
        {
            AudioManager.Instance.SetSFXVolumen(volume);
        }
    }

    public void OnDisable()
    {
        if (AudioManager.Instance)
        {
            SoundVolumenSlider.onValueChanged.RemoveAllListeners();
            FXVolumenSlider.onValueChanged.RemoveAllListeners();
        }
    }
}
