
using System.Collections;
using UnityEngine;

public class SceneLoad : MonoBehaviour {
    public SceneLoader SceneLoader { get; set; }
    public void LoadAfterDelay(Scene scene, float delay) {
        StartCoroutine(LoadScene(scene, delay));
    }

    IEnumerator LoadScene(Scene scene, float delay) {
        yield return new WaitForSeconds(delay);
        SceneLoader.LoadScene(scene);
    }

}
