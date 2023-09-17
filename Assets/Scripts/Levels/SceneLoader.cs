using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneLoader/SceneLoader")]
public class SceneLoader : ScriptableObject {
    public void QuitGame() {
        Application.Quit();
    }

    public void LoadSceneOnIndex(int index) {
        SceneManager.LoadScene(index);
    }

    public void RestartCurrentScene() {
        var currentScene = SceneManager.GetActiveScene();
        var sceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
