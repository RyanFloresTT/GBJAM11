using System;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Key : MonoBehaviour, IAmItem
{
    public static Action OnPlayerPickupKey;
    Player playerCollision;

    private void Start() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null) { return; }
        DoPickUp();
    }

    public void DoPickUp() {
        OnPlayerPickupKey?.Invoke();
        Destroy(gameObject);
    }
}
