using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource OneShotSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;

    public float MusicVolumen;
    public float SFXVolumen;

    public AudioMixer audioMixer;

    public AudioClip mainMenuMusic;
    public AudioClip[] onPuzzleComplete = new AudioClip[2];
    public AudioClip[] levelMusic = new AudioClip[5];

    public delegate void OnMusicVolumenChange(float newVolumen);
    public OnMusicVolumenChange onMusicVolumenChanged;

    public delegate void OnSFXVolumenChange(float newVolumen);
    public OnSFXVolumenChange onSFXVolumenChanged;

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        OneShotSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        LoadPlayerPrefs();
        PlayAmbience();
    }

    public void LoadPlayerPrefs() 
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolumen(PlayerPrefs.GetFloat("MusicVolume"));
        }

        if (PlayerPrefs.HasKey("FXVolume"))
        {
            SetSFXVolumen(PlayerPrefs.GetFloat("FXVolume"));
        }
    }

    public void SavePlayerPrefs() 
    {
        PlayerPrefs.SetFloat("MusicVolumen", MusicVolumen);
        PlayerPrefs.SetFloat("FXVolume", SFXVolumen);
    }

    public void PlayRandomMusic() 
    {
        int randomMusicInt = Random.Range(0, levelMusic.Length);

        if (!musicSource)
        {
            Debug.LogError("Trying to play music when an AudioSource has not been set.", transform);
            return;
        }

        //musicSource.volume = (PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 50f) / 100f;
        musicSource.clip = levelMusic[randomMusicInt];
        musicSource.Play();
    }

    public void PlayMusic(AudioClip musicToPlay) {
        if (!musicSource) {
            Debug.LogError("Trying to play music when an AudioSource has not been set.", transform);
            return;
        }

        musicSource.clip = musicToPlay;
        musicSource.Play();
    }

    public void StopMusic() 
    {
        musicSource.Stop();
    }

    public void PlayMainMenuMusic()
    {
        if (!musicSource)
        {
            Debug.LogError("Trying to play music when an AudioSource has not been set.", transform);
            return;
        }

        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }

    public void PlayAmbience() {

        if (!musicSource) {
            Debug.LogError("Trying to play ambience when an AudioSource has not been set.", transform);
            return;
        }

        ambienceSource.Play();
    }

    public void SetMusicVolumen(float volumen) 
    {
        MusicVolumen = volumen;

        //If we on lowest val on slider, we just mute
        if (MusicVolumen <= -30)
        {
            MusicVolumen = -80;
        }

        //if (onMusicVolumenChanged != null) 
        //{
        //    onMusicVolumenChanged(volumen);
        //}

        audioMixer.SetFloat("MusicVolumen", MusicVolumen);

        SavePlayerPrefs();
    }
    public void SetSFXVolumen(float volumen) 
    {
        SFXVolumen = volumen;

        //If we on lowest val on slider, we just mute
        if (SFXVolumen <= -30) 
        {
            SFXVolumen = -80;
        }

        //if (onSFXVolumenChanged != null)
        //{
        //    onSFXVolumenChanged(volumen);
        //}

        audioMixer.SetFloat("SFXVolumen", SFXVolumen);

        SavePlayerPrefs();
    }

    public void PlayOnPuzzleComplete()
    {
        int randomNum = Random.Range(0, 1);
        musicSource.PlayOneShot(onPuzzleComplete[randomNum]);
    }

    public void PlayOneShotSound(AudioClip audio) 
    {
        OneShotSource.PlayOneShot(audio);
    }
}