using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleManager : MonoBehaviour {
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private UIDocument uiDoc;
    private VisualElement rootEl;
    private VisualElement startText;
    private string activeClass = "active";

    private void OnEnable() {
        InputHandler.OnStartPressed += HandleStart;
        InputHandler.OnAPressed += HandleStart;
        InputHandler.OnBPressed += HandleStart;
        InputHandler.OnSelectPressed += HandleStart;

        rootEl = uiDoc.rootVisualElement;
        startText = rootEl.Q("title-start");
    }

    private void OnDisable() {
        InputHandler.OnStartPressed -= HandleStart;
        InputHandler.OnAPressed -= HandleStart;
        InputHandler.OnBPressed -= HandleStart;
        InputHandler.OnSelectPressed -= HandleStart;
    }

    private void HandleStart() {
        sceneLoader.LoadScene(Scene.Demo);
    }

    private void Start() {
        StartCoroutine(ToggleActiveClass());
    }

    private IEnumerator ToggleActiveClass() {
        bool isActive = true; // Initially set to true

        while (true) {
            startText.ToggleInClassList(activeClass);
            isActive = !isActive;
            yield return new WaitForSeconds(isActive ? 2f : 0.25f);
        }
    }

}
