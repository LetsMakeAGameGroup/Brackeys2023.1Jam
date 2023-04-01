using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource OneShotSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;

    float MusicVolumen = 0;
    float SFXVolumen = 0;

    public AudioMixer audioMixer;

    public AudioClip mainMenuMusic;
    public AudioClip[] onPuzzleComplete = new AudioClip[2];
    public AudioClip[] levelMusic = new AudioClip[5];

    //Settings

    public Slider MusicVolumenSlider;
    public Slider FXVolumenSlider;

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
        if (PlayerPrefs.HasKey("MusicVolumen"))
        {
            SetMusicVolumen(PlayerPrefs.GetFloat("MusicVolumen"));
        }

        if (PlayerPrefs.HasKey("SFXVolumen"))
        {
            SetSFXVolumen(PlayerPrefs.GetFloat("SFXVolumen"));
        }

        MusicVolumenSlider.value = MusicVolumen;
        FXVolumenSlider.value = SFXVolumen;
            
        MusicVolumenSlider.onValueChanged.AddListener(SetMusicVolumen);
        FXVolumenSlider.onValueChanged.AddListener(SetSFXVolumen);
    }

    public void PlayRandomMusic() 
    {
        int randomMusicInt = Random.Range(0, levelMusic.Length);

        if (!musicSource)
        {
            Debug.LogError("Trying to play music when an AudioSource has not been set.", transform);
            return;
        }

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

        audioMixer.SetFloat("MusicVolumen", MusicVolumen);
        PlayerPrefs.SetFloat("MusicVolumen", MusicVolumen);
    }
    public void SetSFXVolumen(float volumen) 
    {
        SFXVolumen = volumen;
        
        //If we on lowest val on slider, we just mute
        if (SFXVolumen <= -30) 
        {
            SFXVolumen = -80;
        }

        audioMixer.SetFloat("SFXVolumen", SFXVolumen);

        PlayerPrefs.SetFloat("SFXVolumen", SFXVolumen);
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