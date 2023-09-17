using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesWithInput : MonoBehaviour
{
    [SerializeField] float moveSpeed = .125f;

    void Update() {
        Debug.Log(InputHandler.GetAnalogVectorNormalized());
        transform.position += (InputHandler.GetAnalogVectorNormalized() * moveSpeed).ToVector3();
    }
}
