using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    // Singleton instance
    public static MusicPlayer Instance { get; private set; }

    // Music audio data files
    [SerializeField] private MusicManagerSO musicData;

    // Audio sources for intro/main
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource mainSource;

    // Settings
    [SerializeField] private float fadeDuration = 1.0f;

    // State
    private MusicTrack currentTrack;

    private double scheduledTime;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        musicData.Initialize();
        mainSource.loop = true;
    }

    /*private void Start() {
         StartCoroutine(PlaySong(SongName.Battle));
    }*/

    private double getIntroPlaytime() {
        return ((double)introSource.timeSamples / introSource.clip.frequency);
    }

    private void Update() {
        if (introSource.isPlaying) {
            mainSource.SetScheduledStartTime(AudioSettings.dspTime + currentTrack.introDuration - getIntroPlaytime());
        }
    }

    public IEnumerator PlaySong(SongName song) {
        // Stop the current song if any
        StopCurrentSong();

        // Fetch the audio files
        MusicTrack track = musicData.FetchSong(song);
        currentTrack = track;

        mainSource.clip = currentTrack.Main;
        introSource.clip = currentTrack.Intro;

        // If it has an intro play that first
        if (currentTrack.Intro != null) {
            introSource.Play();
            mainSource.PlayScheduled(AudioSettings.dspTime + track.introDuration);
        } else {
            // Otherwise go straight to main
            mainSource.Play();
        }

        yield return null;
    }

    public IEnumerator StopCurrentSong() {
        if (introSource.isPlaying) {
            yield return StartCoroutine(FadeOut(introSource));
        }

        if (mainSource.isPlaying) {
            yield return StartCoroutine(FadeOut(mainSource));
        }

        // Cancel any scheduled plays
        mainSource.Stop();
        introSource.Stop();
    }

    private IEnumerator FadeOut(AudioSource audioSource) {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
