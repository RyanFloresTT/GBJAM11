using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesWithInput : MonoBehaviour {
    [SerializeField] float moveSpeed = .125f;

    void Update() {
        transform.position += (InputHandler.GetAnalogVectorNormalized() * moveSpeed * Time.deltaTime).ToVector3();
    }
}
