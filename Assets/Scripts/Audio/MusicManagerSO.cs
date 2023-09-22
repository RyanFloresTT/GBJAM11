using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicTrack {
    public SongName Song;
    public AudioClip Intro;
    public double introDuration;
    public AudioClip Main;
    public double mainDuration;
    public bool loopSong;
}

public enum SongName {
    Title,
    Credits,
    Menu,
    Dungeon,
    Battle,
}


[CreateAssetMenu(fileName = "MusicManager", menuName = "ScriptableObjects/Managers/MusicManager")]
public class MusicManagerSO : ScriptableObject {
    [SerializeField] private List<MusicTrack> musicList;
    private bool initialized = false;

    public void Initialize() {
        if (initialized) return;

        for (int i = 0; i < musicList.Count; i++) {
            var track = musicList[i];

            if (track.Intro == null) {
                track.introDuration = 0;
            } else {
                track.introDuration = clipDuration(track.Intro);
            }

            track.mainDuration = clipDuration(track.Main);
            musicList[i] = track;
        }

        initialized = true;
    }
    private double clipDuration(AudioClip clip) {
        double duration = clip.samples / clip.frequency;
        Debug.Log($"Clip: {clip.name}, Duration: {duration}");
        return duration;
    }

    public MusicTrack FetchSong(SongName song) => musicList.Find(t => t.Song == song);
}
