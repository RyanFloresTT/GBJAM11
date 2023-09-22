using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveableCrate : MonoBehaviour {
    [SerializeField] float soundInterval = 1f;

    public static Action OnObjectMoved;

    Rigidbody2D rb2D;
    Vector2 lastPosition;
    float timeSinceLastSound = 0f;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        lastPosition = rb2D.position;
    }

    private void Update() {
        Vector2 currentPosition = rb2D.position;

        if (Vector2.Distance(currentPosition, lastPosition) > 0.01f) {
            timeSinceLastSound += Time.deltaTime;

            if (timeSinceLastSound >= soundInterval) {
                OnObjectMoved?.Invoke();
                timeSinceLastSound = 0f;
            }
        }

        lastPosition = currentPosition;
    }
}
