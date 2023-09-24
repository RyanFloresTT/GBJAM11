using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackLocation : MonoBehaviour
{
    Vector3 lastPosition = Vector2.zero;
    private void Update() {
        lastPosition = InputHandler.GetAnalogVectorNormalized().ToVector3();
        if (lastPosition != Vector3.zero) {
            transform.localPosition = lastPosition;
        }
    }
}
