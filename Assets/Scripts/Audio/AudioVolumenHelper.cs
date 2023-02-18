using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType 
{
    SoundFX,
    Music
}
public class AudioVolumenHelper : MonoBehaviour
{
    AudioSource m_AudioSource;
    public AudioType soundType;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load sounds from settings
        switch (soundType)
        {
            case AudioType.SoundFX:

                if (PlayerPrefs.HasKey("FXVolume"))
                {
                    SetVolume(PlayerPrefs.GetFloat("FXVolume"));
                }

                break;
            case AudioType.Music:

                if (PlayerPrefs.HasKey("MusicVolume"))
                {
                    SetVolume(PlayerPrefs.GetFloat("MusicVolume"));
                }

                break;
        }


        if (AudioManager.Instance) 
        {
            switch (soundType) 
            {
                case AudioType.SoundFX:
                    AudioManager.Instance.onSFXVolumenChanged += SetVolume;
                    break;
                case AudioType.Music:
                    AudioManager.Instance.onMusicVolumenChanged += SetVolume;
                    break;
            }
        }
    }

    void OnEnable()
    {
        //Load sounds from settings
        switch (soundType)
        {
            case AudioType.SoundFX:

                if (PlayerPrefs.HasKey("FXVolume"))
                {
                    SetVolume(PlayerPrefs.GetFloat("FXVolume"));
                }

                break;
            case AudioType.Music:

                if (PlayerPrefs.HasKey("MusicVolume"))
                {
                    SetVolume(PlayerPrefs.GetFloat("MusicVolume"));
                }

                break;
        }


        if (AudioManager.Instance)
        {
            switch (soundType)
            {
                case AudioType.SoundFX:
                    AudioManager.Instance.onSFXVolumenChanged += SetVolume;
                    break;
                case AudioType.Music:
                    AudioManager.Instance.onMusicVolumenChanged += SetVolume;
                    break;
            }
        }
    }

    private void OnDisable()
    {
        if (AudioManager.Instance)
        {
            switch (soundType)
            {
                case AudioType.SoundFX:
                    AudioManager.Instance.onSFXVolumenChanged -= SetVolume;
                    break;
                case AudioType.Music:
                    AudioManager.Instance.onMusicVolumenChanged -= SetVolume;
                    break;
            }
        }
    }

    public void SetVolume(float newVolumen) 
    {
        m_AudioSource.volume = newVolumen;
    }
}
