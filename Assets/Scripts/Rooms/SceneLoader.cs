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

    public void RestartCurrentScene() {
        LoadScene(currentScene);
    }
}
