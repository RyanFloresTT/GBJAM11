using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene {
    Title,
    Demo,
}

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneLoader/SceneLoader")]
public class SceneLoader : ScriptableObject {
    private Scene currentScene = Scene.Title;

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadScene(Scene scene) {
        // Add a temporary delegate to play the song after the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        switch (scene) {
            case Scene.Title:
                SceneManager.LoadSceneAsync(0);
                break;
            case Scene.Demo:
                SceneManager.LoadSceneAsync(1);
                break;
        }

        currentScene = scene;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
        // Remove the temporary delegate
        SceneManager.sceneLoaded -= OnSceneLoaded;

        switch (currentScene) {
            case Scene.Title:
                MusicPlayer.Instance.PlaySong(SongName.Title);
                break;
            case Scene.Demo:
                MusicPlayer.Instance.PlaySong(SongName.Dungeon);
                break;
        }
    }

    public void RestartCurrentScene() {
        LoadScene(currentScene);
    }
}
