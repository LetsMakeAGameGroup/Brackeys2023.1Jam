using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource OneShotSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;

    public float MusicVolumen;
    public float SFXVolumen;

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

        PlayAmbience();

        OneShotSource = GetComponent<AudioSource>();
    }

    private void Start()
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

    public void PlayMusic(AudioClip musicToPlay) {
        if (!musicSource) {
            Debug.LogError("Trying to play music when an AudioSource has not been set.", transform);
            return;
        }

        //musicSource.volume = (PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 50f) / 100f;
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

        ambienceSource.volume = (PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 50f) / 100f;
        ambienceSource.Play();
    }

    public void SetMusicVolumen(float volumen) 
    {
        MusicVolumen = volumen;

        if (onMusicVolumenChanged != null) 
        {
            onMusicVolumenChanged(volumen);
        }

        musicSource.volume = volumen;
    }
    public void SetSFXVolumen(float volumen) 
    {
        SFXVolumen = volumen;

        if (onSFXVolumenChanged != null)
        {
            onSFXVolumenChanged(volumen);
        }

        ambienceSource.volume = volumen;
    }

    public void PlayOnPuzzleComplete()
    {
        int randomNum = Random.Range(0, 1);

        OneShotSource.volume = SFXVolumen;
        OneShotSource.PlayOneShot(onPuzzleComplete[randomNum]);
    }
}