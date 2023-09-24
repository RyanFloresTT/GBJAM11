using Unity.VisualScripting;
using UnityEngine;

public class ScrollingCredits : MonoBehaviour {
    private float creditsStart = -496;
    private float thanksStart = -240;
    private float logoStart = -160;
    private float creditsHeight = 500;
    private float thanksHeight = -64;
    private float moveSpeed = 64f;

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject skipText;
    [SerializeField] private GameObject thanks;
    [SerializeField] private GameObject gameLogo;
    [SerializeField] private MusicPlayer music;
    [SerializeField] private SceneLoader sceneLoader;

    private float timeSinceStartPressed = -1;
    private Vector3 skipTextShowPosition;
    private Vector3 skipTextHidePosition;

    private void OnEnable() {
        music = MusicPlayer.Instance;

        InputHandler.OnStartPressed += HandleStart;
        InputHandler.OnAPressed += HandleStart;
        InputHandler.OnBPressed += HandleStart;
        InputHandler.OnSelectPressed += HandleStart;
    }

    private void OnDisable() {
        InputHandler.OnStartPressed -= HandleStart;
        InputHandler.OnAPressed -= HandleStart;
        InputHandler.OnBPressed -= HandleStart;
        InputHandler.OnSelectPressed -= HandleStart;
    }

    private void HandleStart() {
        if (timeSinceStartPressed >= 0) {
            sceneLoader.LoadScene(Scene.SoundTest);
        } else {
            timeSinceStartPressed = 0;
            ShowSkipMessage();
        }
    }

    private void SetStartPosition(GameObject scrollObject, float startY) {
        if (scrollObject != null) {
            Vector3 startPosition = scrollObject.transform.position;
            startPosition.y = startY;
            scrollObject.transform.position = startPosition;
        }
    }

    public void Start() {
        skipTextShowPosition = skipText.transform.position;
        skipTextHidePosition = skipTextShowPosition;
        skipTextHidePosition.y -= 100;
        skipText.transform.position = skipTextHidePosition;

        SetStartPosition(credits, creditsStart);
        SetStartPosition(thanks, thanksStart);
        SetStartPosition(gameLogo, logoStart);

        if (music != null) {
            music.PlaySong(SongName.Credits);
        }
    }

    private void ScrollObject(GameObject scrollObject, float moveAmount) {        
        if (scrollObject != null) {
            Vector3 newPosition = scrollObject.transform.position;
            newPosition.y += moveAmount;
            scrollObject.transform.position = newPosition;
        }
    }

    private void ShowSkipMessage() {
        skipText.transform.position = skipTextShowPosition;
    }

    private void HideSkipMessage() {
        skipText.transform.position = skipTextHidePosition;
    }

    private void UpdateSkipTimer() {
        if (timeSinceStartPressed > 3) {
            timeSinceStartPressed = -1;
            HideSkipMessage();
        } else if (timeSinceStartPressed >= 0) {
            timeSinceStartPressed += Time.deltaTime;
        }
    }

    public void Update() {
        UpdateSkipTimer();

        float moveAmount = moveSpeed * Time.deltaTime;

        if (credits != null && credits.GetComponent<RectTransform>().anchoredPosition.y < creditsHeight) {
            ScrollObject(gameLogo, moveAmount);
            ScrollObject(credits, moveAmount);
        } else if (thanks.GetComponent<RectTransform>().anchoredPosition.y < thanksHeight) {
            ScrollObject(thanks, moveAmount);
        }
    }
}
