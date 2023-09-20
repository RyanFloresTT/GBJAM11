using UnityEngine;

public class PressurePlate : MonoBehaviour, IPuzzleObject {

    [SerializeField] LayerMask crateLayer;
    [SerializeField] float overlapThreshold = 0.5f;

    bool isSolved;

    Collider2D plateCollider;
    Bounds plateBounds;

    bool IPuzzleObject.PuzzlePieceSolved => isSolved;

    private void Awake() {
        plateCollider = GetComponent<Collider2D>();
        plateBounds = plateCollider.bounds;
        isSolved = false;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (IsCrateOverPlate(other)) {
            isSolved = true;
        }
    }

    bool IsObjectMostlyOverPressurePlate(Collider2D other) {
        Bounds objectBounds = other.bounds;
        float overlapArea = Mathf.Max(0, Mathf.Min(plateBounds.max.x, objectBounds.max.x) - Mathf.Max(plateBounds.min.x, objectBounds.min.x)) *
                            Mathf.Max(0, Mathf.Min(plateBounds.max.y, objectBounds.max.y) - Mathf.Max(plateBounds.min.y, objectBounds.min.y));
        float objectArea = objectBounds.size.x * objectBounds.size.y;
        return overlapArea >= overlapThreshold * objectArea;
    }

    bool IsCrateOverPlate(Collider2D other) => ((1 << other.gameObject.layer) & crateLayer) > 0 && IsObjectMostlyOverPressurePlate(other);
}
