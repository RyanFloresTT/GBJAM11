using UnityEngine;
using UnityEngine.SceneManagement;
    
public enum Scene {
    Title,
    Demo,
    Credits,
    SoundTest,
    Level1,
}

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneLoader/SceneLoader")]
public class SceneLoader : ScriptableObject {
    private Scene currentScene = Scene.Title;
    SceneLoad sceneLoad;

    public void InitSceneLoad(GameObject caller) {
        Debug.Log("sceneLoad loaded");
        caller.TryGetComponent(out sceneLoad);
        sceneLoad.SceneLoader = this;
    }

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
            case Scene.Level1:
                SceneManager.LoadSceneAsync(1);
                break;
            case Scene.Credits:
                SceneManager.LoadSceneAsync(2);
                break;
            case Scene.SoundTest:
                SceneManager.LoadSceneAsync(3);
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
            case Scene.Level1:
                MusicPlayer.Instance.PlaySong(SongName.Dungeon);
                break;
        }
    }

    public void LoadSceneAfterDelay(Scene scene, float delay) {
        sceneLoad.LoadAfterDelay(scene, delay);
    }

    public void RestartCurrentScene() {
        var currentScene = SceneManager.GetActiveScene();
        var sceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
