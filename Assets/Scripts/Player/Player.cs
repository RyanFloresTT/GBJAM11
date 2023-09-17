using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHaveHealth
{
    public int Health { get; set; }
    [SerializeField] int MaxHealth;

    public static Action<int> OnPlayerHealthUpdate;

    void Start() {
        ModifyHealth(MaxHealth);
    }

    public void ModifyHealth(int h) {
        if (h >= 0) {
            Health = Health + h > MaxHealth ? MaxHealth : Health += h;
        } else {
            Health += h;
        }
        OnPlayerHealthUpdate?.Invoke(Health);
        if (Health <= 0) { OnDeath(); }
    }

    public void OnDeath() {
        Destroy(gameObject);
    }
}
