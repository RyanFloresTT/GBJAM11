using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtPlayerOnContact : MonoBehaviour
{
    [SerializeField] int contactDamage;
    Player playerCollision;

    private void Start() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null) { return; }
        playerCollision = player;
        HurtPlayer();   
    }

    void HurtPlayer() {
        playerCollision.ModifyHealth(contactDamage * - 1);
    }
}
