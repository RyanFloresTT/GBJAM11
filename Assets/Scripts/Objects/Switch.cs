using UnityEngine;

public class Switch : MonoBehaviour, IInteractable, IPuzzleObject {
    private bool puzzlePieceSolved = false;

    [SerializeField] SpriteRenderer onSprite;
    [SerializeField] SpriteRenderer offSprite;
    [SerializeField] LayerMask playerLayer;

    bool IPuzzleObject.PuzzlePieceSolved => puzzlePieceSolved;

    private void Toggle() {
        onSprite.enabled = !onSprite.enabled;
        puzzlePieceSolved = !puzzlePieceSolved;
    }

    public void OnInteract() {
        Toggle();
    }
}
