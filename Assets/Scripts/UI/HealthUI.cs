using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject healthIcon;

    void Awake() {
        Player.OnPlayerHealthUpdate += PlayerHealthUpdated;
    }

    void PlayerHealthUpdated(int health) {
        // Can prob optimzie this better :P
        foreach (Transform child in transform) { Destroy(child.gameObject); }
        for (int i = 0; i < health; i++) {
            Instantiate(healthIcon, transform);
        }
    }
}
