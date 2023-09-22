using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHaveHealth, IPuzzleObject {
    public int Health { get; set; }
    public bool HasKey { get; set; }

    [SerializeField] int MaxHealth;

    public static Action<int> OnPlayerHealthUpdate;

    void Start() {
        ModifyHealth(MaxHealth);
        HasKey = false;
        Key.OnPlayerPickupKey += Handle_PlayerPickupKey;
    }

    private void Handle_PlayerPickupKey() {
        HasKey = true;
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
    public bool PuzzlePieceSolved => HasKey;
}
