using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesWithInput : MonoBehaviour {
    [SerializeField] float moveSpeed = .125f;

    public Vector2 CurrentDirection { get; private set; } = Vector2.zero;

    void Update() {
        Vector2 direction = InputHandler.GetAnalogVectorNormalized();
        transform.position += (direction * moveSpeed * Time.deltaTime).ToVector3();

        // Keeps track of which direction the player is looking
        if (direction != Vector2.zero) {
            CurrentDirection = direction;
        }
    }
}
