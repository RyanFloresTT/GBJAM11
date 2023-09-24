using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundTestManager : MonoBehaviour {
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private SFXPlayer sfxPlayer;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject songNameText;
    [SerializeField] private GameObject sfxNameText;

    private TextMeshProUGUI songNameTMP;
    private TextMeshProUGUI sfxNameTMP;

    private MusicManagerSO musicData;

    private PlayerInputActions playerInput;

    private int currentCursorPosition = 0;
    private int currentMusicTrack = 0;
    private int currentSfx = 0;

    private Vector3[] cursorPositions = {
        new Vector3(4,-24,0),
        new Vector3(4,-64,0),
        new Vector3(14,-126,0),
    };

    void OnEnable() {
        musicPlayer = MusicPlayer.Instance;
        musicPlayer.StopCurrentSong();

        sfxNameTMP = sfxNameText.GetComponent<TextMeshProUGUI>();
        sfxNameTMP.text = ClipText(currentSfx);

        playerInput = new PlayerInputActions();

        playerInput.Player.Enable();

        playerInput.Player.UpMenu.performed += Handle_Player_MenuUp;
        playerInput.Player.DownMenu.performed += Handle_Player_MenuDown;
        playerInput.Player.RightMenu.performed += Handle_Player_MenuRight;
        playerInput.Player.LeftMenu.performed += Handle_Player_MenuLeft;

        playerInput.Player.A.performed += HandleStart;
        playerInput.Player.B.performed += HandleStop;
        playerInput.Player.Start.performed += HandleStart;
        playerInput.Player.Select.performed += HandleStop;
    }

    private string ClipText(int i) {
        string[] textParts = sfxPlayer.ClipName(currentSfx).Split('-');
        string text = textParts[0];
        if (textParts.Length > 1) {
            text += " " + textParts[1];
        }
        if (textParts.Length == 3) {
            text += "\n    " + textParts[2];
        }

        return text;
    }

    private void Start() {
        musicData = musicPlayer.musicData;

        songNameTMP = songNameText.GetComponent<TextMeshProUGUI>();
        songNameTMP.text = musicData.FetchSongByIndex(currentMusicTrack).Name;

        cursor.GetComponent<RectTransform>().anchoredPosition = cursorPositions[currentCursorPosition];
    }

    void HandleStop(InputAction.CallbackContext obj) {
        musicPlayer.StopCurrentSong();
    }

    void HandleStart(InputAction.CallbackContext obj) {
        if (currentCursorPosition == 0) {
            musicPlayer.PlaySong(musicData.FetchSongByIndex(currentMusicTrack).Song);
        } else if (currentCursorPosition == 1) {
            sfxPlayer.PlayClipByIndex(currentSfx);
        } else if (currentCursorPosition == 2) {
            sceneLoader.LoadScene(Scene.Title);
        }
    }

    void Handle_Player_MenuUp(InputAction.CallbackContext obj) {
        if (currentCursorPosition == 0) {
            currentCursorPosition = cursorPositions.Length - 1;
        } else {
            currentCursorPosition -= 1;
        }
        cursor.GetComponent<RectTransform>().anchoredPosition = cursorPositions[currentCursorPosition];
    }

    void Handle_Player_MenuDown(InputAction.CallbackContext obj) {
        currentCursorPosition = (currentCursorPosition + 1) % cursorPositions.Length;
        cursor.GetComponent<RectTransform>().anchoredPosition = cursorPositions[currentCursorPosition];
    }

    void Handle_Player_MenuRight(InputAction.CallbackContext obj) {
        if (currentCursorPosition == 0) {
            currentMusicTrack = (currentMusicTrack + 1) % musicData.Tracks();
            songNameTMP.text = musicData.FetchSongByIndex(currentMusicTrack).Name;
        } else if (currentCursorPosition == 1) {
            currentSfx = (currentSfx + 1) % sfxPlayer.Clips();
            sfxNameTMP.text = ClipText(currentSfx);
        }
    }

    void Handle_Player_MenuLeft(InputAction.CallbackContext obj) {
        if (currentCursorPosition == 0) {
            if (currentMusicTrack == 0) {
                currentMusicTrack = musicData.Tracks() - 1;
            } else {
                currentMusicTrack -= 1;
            }
            songNameTMP.text = musicData.FetchSongByIndex(currentMusicTrack).Name;
        } else if (currentCursorPosition == 1) {
            if (currentSfx == 0) {
                currentSfx = sfxPlayer.Clips() - 1;
            } else {
                currentSfx -= 1;
            }
            sfxNameTMP.text = ClipText(currentSfx);
        }
    }
}
