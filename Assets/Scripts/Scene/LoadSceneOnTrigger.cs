using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    Collider2D triggerCollider;

    private void Start() {
        triggerCollider = GetComponent<Collider2D>();
        triggerCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        LoadCredits();
    }

    private void LoadCredits() {
        sceneLoader.InitSceneLoad(gameObject);
        sceneLoader.LoadSceneAfterDelay(Scene.Credits, 2f);
    }
}
