using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    [SerializeField] LayerMask interactLayer;
    [SerializeField] MovesWithInput playerMovement;
    [SerializeField] float interactDistance = 1f;
    [SerializeField] Transform interactLocation;
    [SerializeField] float radius;

    CircleCollider2D coll;
    Vector2 attackDirection;

    private void OnEnable() {
        InputHandler.OnAPressed += HandleInteract;
    }

    private void OnDisable() {
        InputHandler.OnAPressed -= HandleInteract;
    }

    private void Start() {
        coll = GetComponent<CircleCollider2D>();
    }

    private void HandleInteract() {
        GameObject hitObject = CheckForHit();
        if (!hitObject) return;

        // See if it's interactable
        IInteractable interactableComponent = hitObject.GetComponent<IInteractable>();

        if (interactableComponent != null) {
            interactableComponent.OnInteract();
        }
    }

    private GameObject CheckForHit() {
        // Use the layer mask in the raycast
        attackDirection = PlayerAnimationState.Direction;
        RaycastHit2D hit = Physics2D.CircleCast(interactLocation.position, radius, attackDirection, interactDistance, interactLayer);
        Debug.DrawLine(coll.bounds.center, coll.bounds.center + (Vector3)playerMovement.CurrentDirection * interactDistance, Color.red, 1.0f);

        if (!hit) {
            return null;
        } else {
            return hit.collider.gameObject;
        }
    }

}
