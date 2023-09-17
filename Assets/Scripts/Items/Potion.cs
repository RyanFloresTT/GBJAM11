using UnityEngine;

// This script breaks DRY, repeats what's in "HurtPlayerOnContact" script but minor tweaks.
[RequireComponent(typeof(Collider2D))]
public class Potion : MonoBehaviour, IAmItem  {
    [SerializeField] int healAmount;
    Player playerCollision;

    private void Start() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null) { return; }
        playerCollision = player;
        DoPickUp();
    }

    public void DoPickUp() {
        playerCollision.ModifyHealth(healAmount);
        Destroy(gameObject);
    }
}
