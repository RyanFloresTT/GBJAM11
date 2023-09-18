using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicTrack {
    public SongName Song;
    public AudioClip Intro;
    public double introDuration;
    public AudioClip Main;
    public double mainDuration;
}

public enum SongName {
    Menu,
    Dungeon,
    Battle,
}


[CreateAssetMenu(fileName = "MusicManager", menuName = "ScriptableObjects/Managers/MusicManager")]
public class MusicManagerSO : ScriptableObject {
    [SerializeField] private List<MusicTrack> musicList;
    private bool initialized = false;

    private double clipDuration(AudioClip clip) {
        return (double)(clip.samples) / (double)clip.frequency;
    }

    public void Initialize() {
        if (initialized) return;

        for (int i = 0; i < musicList.Count; i++) {
            var track = musicList[i];
            track.introDuration = clipDuration(track.Intro);
            track.mainDuration = clipDuration(track.Main);
            musicList[i] = track;
        }

        initialized = true;
    }

    public MusicTrack FetchSong(SongName song) {
        MusicTrack track = musicList.Find(t => t.Song == song);
        return track;
    }
}
