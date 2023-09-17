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
        for (int i = 0; i < health; i++) {
            Debug.Log('p');
            Instantiate(healthIcon, gameObject.transform);
        }
    }
}
