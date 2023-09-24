using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] private MovesWithInput playerMovement;
    [SerializeField] float interactDistance = 1f;
    private CircleCollider2D coll;

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
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, playerMovement.CurrentDirection, interactDistance, ~ignoreMask);
        Debug.DrawLine(coll.bounds.center, coll.bounds.center + (Vector3)playerMovement.CurrentDirection * interactDistance, Color.red, 1.0f);

        if (!hit) {
            return null;
        } else {
            return hit.collider.gameObject;
        }
    }

}
