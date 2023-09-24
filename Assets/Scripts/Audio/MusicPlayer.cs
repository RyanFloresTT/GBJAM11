using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    // Singleton instance
    public static MusicPlayer Instance { get; private set; }

    // Music audio data files
    [SerializeField] public MusicManagerSO musicData;

    // Audio sources for intro/main
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource mainSource;

    // Settings
    [SerializeField] private float fadeDuration = 1.0f;

    // State
    private MusicTrack currentTrack;
    private bool mainSourceScheduled = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        musicData.Initialize();
    }

    /*
    private void Start() {
        PlaySong(SongName.Title);
    }*/

    private double GetIntroPlaytime() {
        return (double)introSource.timeSamples / (double)introSource.clip.frequency;
    }

    private bool shouldScheduleMainTrack() {
        // There is a main clip to be played, it isn't empty, it hasn't been scheduled yet, and it isn't playing.
        return (!mainSourceScheduled &&
            currentTrack.Main != null && currentTrack.mainDuration > 0 && !mainSource.isPlaying);
    }

    private bool timeToScheduleMainTrack() {
        // The intro source is playing, and we're more than halfway through it
        return (introSource.isPlaying && (introSource.timeSamples * 2 > currentTrack.Intro.samples));
    }

    private void Update() {
        if (shouldScheduleMainTrack() && timeToScheduleMainTrack()) {
            mainSource.PlayScheduled(AudioSettings.dspTime + currentTrack.introDuration - GetIntroPlaytime());
            mainSourceScheduled = true;
        }
    }

    public void PlaySong(SongName song) {
        StartCoroutine(PlaySongRoutine(song));
    }

    private void OnLevelWasLoaded(int level) {
       switch (level) {
            // play main
            case 1:
                PlaySong(SongName.Dungeon);
                break;
        }
    }

    private IEnumerator PlaySongRoutine(SongName song) {
        // Stop the current song if any
        mainSource.Stop();
        introSource.Stop();

        // Fetch the audio files
        MusicTrack track = musicData.FetchSong(song);
        currentTrack = track;

        mainSource.clip = currentTrack.Main;
        mainSource.loop = currentTrack.loopSong;
        introSource.clip = currentTrack.Intro;

        // If it has an intro play that first
        if (currentTrack.Intro != null) {
            introSource.Play();
            mainSourceScheduled = false;
        } else {
            // Otherwise go straight to main
            mainSourceScheduled= true;
            mainSource.Play();
        }

        yield return null;
    }

    public void StopCurrentSong() {
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
