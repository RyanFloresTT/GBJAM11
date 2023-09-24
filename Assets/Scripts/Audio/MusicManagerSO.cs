using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicTrack {
    public SongName Song;
    public string Name;
    public AudioClip Intro;
    public double introDuration;
    public AudioClip Main;
    public double mainDuration;
    public bool loopSong;
}

public enum SongName {
    Title,
    Dungeon,
    Battle,
    Credits,
}


[CreateAssetMenu(fileName = "MusicManager", menuName = "ScriptableObjects/Managers/MusicManager")]
public class MusicManagerSO : ScriptableObject {
    [SerializeField] private List<MusicTrack> musicList;

    public void Initialize() {
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

    }
    private double clipDuration(AudioClip clip) {
        double duration = (double)clip.samples / (double)clip.frequency;
        //Debug.Log($"Clip: {clip.name}, Duration: {duration}");
        return duration;
    }

    public MusicTrack FetchSong(SongName song) {
       return musicList.Find(t => t.Song == song);
    }

    public MusicTrack FetchSongByIndex(int songNumber) {
        return musicList[songNumber];
    }

    public int Tracks() {
        return musicList.Count;
    }
}
