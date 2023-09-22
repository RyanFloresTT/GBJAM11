using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject chooseCanvas;

    private void Start() {
        MissingShapeUI.OnPlayerFinishedChoosing += Handle_PlayerFinishedChoosing;
    }

    private void Handle_PlayerFinishedChoosing() {
        chooseCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
    }
}
