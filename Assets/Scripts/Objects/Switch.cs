using System;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable, IPuzzleObject {
    private bool puzzlePieceSolved = false;

    [SerializeField] SpriteRenderer onSprite;
    [SerializeField] SpriteRenderer offSprite;
    [SerializeField] LayerMask playerLayer;

    public static Action OnPlayerUsedSwitch;

    bool IPuzzleObject.PuzzlePieceSolved => puzzlePieceSolved;

    private void Toggle() {
        onSprite.enabled = !onSprite.enabled;
        puzzlePieceSolved = !puzzlePieceSolved;
        OnPlayerUsedSwitch?.Invoke();
    }

    public void OnInteract() {
        Toggle();
    }
}
