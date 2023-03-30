using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsSettings : MonoBehaviour
{
    public float musicVolumen;
    public float fxVolumen;

    public Slider SoundVolumenSlider;
    public Slider FXVolumenSlider;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicVolumeSlider"))
        {
            musicVolumen = PlayerPrefs.GetFloat("MusicVolumeSlider");
            SoundVolumenSlider.value = musicVolumen;
        }

        if (PlayerPrefs.HasKey("FXVolumeSlider"))
        {
            fxVolumen = PlayerPrefs.GetFloat("FXVolumeSlider");
            FXVolumenSlider.value = fxVolumen;
        }

        if (AudioManager.Instance)
        {
            SoundVolumenSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolumen);
            FXVolumenSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolumen);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolumen = SoundVolumenSlider.value;
        PlayerPrefs.SetFloat("MusicVolumeSlider", musicVolumen);

        if (AudioManager.Instance) 
        {
            AudioManager.Instance.SetMusicVolumen(musicVolumen);
        }
    }

    public void SetFXVolume(float volume)
    {
        fxVolumen = FXVolumenSlider.value;
        PlayerPrefs.SetFloat("FXVolumeSlider", fxVolumen);

        if (AudioManager.Instance)
        {
            AudioManager.Instance.SetSFXVolumen(fxVolumen);
        }
    }

    public void OnDisable()
    {
        SoundVolumenSlider.onValueChanged.RemoveAllListeners();
        FXVolumenSlider.onValueChanged.RemoveAllListeners();
    }
}
