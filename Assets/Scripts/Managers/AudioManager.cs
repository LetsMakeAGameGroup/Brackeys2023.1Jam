using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        PlayMusic();
        PlayAmbience();
    }

    public void PlayMusic() {
        if (!musicSource) {
            Debug.LogError("Trying to play music when an AudioSource has not been set.", transform);
            return;
        }

        musicSource.volume = (PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 50f) / 100f;
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
}