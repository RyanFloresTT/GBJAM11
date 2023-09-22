using System;
using UnityEngine;

public class PlayerWalkThroughDoor : MonoBehaviour {

    [SerializeField] Directions entranceDirection;
    public Action OnWalkThroughEntrance;

    private void OnTriggerExit2D(Collider2D collision) {
        Vector2 exitDirection = collision.transform.position - transform.position;

        switch (entranceDirection) {
            case Directions.North:
                if (exitDirection.y <= 0) { WalkedThroughDoor(); }
                break;
            case Directions.South:
                if (exitDirection.y >= 0) { WalkedThroughDoor(); }
                break;
            case Directions.East:
                if (exitDirection.x <= 0) { WalkedThroughDoor(); }
                break;
            case Directions.West:
                if (exitDirection.x >= 0) { WalkedThroughDoor(); }
                break;
            default:
                Debug.LogWarning("Direction Enum Defaulted!");
                break;
        }
    }

    void WalkedThroughDoor() {
        OnWalkThroughEntrance?.Invoke();
        Destroy(gameObject);
    }
}
