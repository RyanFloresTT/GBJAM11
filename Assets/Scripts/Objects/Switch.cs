using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    bool on = false;
    [SerializeField] SpriteRenderer onSprite;
    [SerializeField] SpriteRenderer offSprite;
    [SerializeField] LayerMask playerLayer;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void Toggle() {
        onSprite.enabled = !onSprite.enabled;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer))) {
            Toggle();
        }
    }
}
