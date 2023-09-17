using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicTrack {
    public SongName Song;
    public AudioClip Intro;
    public AudioClip Main;
}

public enum SongName {
    Menu,
    Dungeon,
    Battle,
}


[CreateAssetMenu(fileName = "MusicManager", menuName = "ScriptableObjects/Managers/MusicManager")]
public class MusicManagerSO : ScriptableObject {
    [SerializeField] private List<MusicTrack> musicList;

    public MusicTrack FetchSong(SongName song) {
        MusicTrack track = musicList.Find(t => t.Song == song);
        return track;
    }
}
