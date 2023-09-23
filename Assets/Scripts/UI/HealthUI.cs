using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] GameObject healthIcon;
    [SerializeField] Player player;

    void Awake() {
        Player.OnPlayerHealthUpdate += PlayerHealthUpdated;
    }

    private void OnEnable() {
        PlayerHealthUpdated(player.Health);
    }

    void PlayerHealthUpdated(int health) {
        // Can prob optimzie this better :P
        foreach (Transform child in transform) { Destroy(child.gameObject); }
        for (int i = 0; i < player.Health; i++) {
            Instantiate(healthIcon, transform);
        }
    }
}
