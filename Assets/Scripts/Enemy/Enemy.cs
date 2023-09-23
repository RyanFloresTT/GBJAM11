using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHaveHealth {
    [SerializeField] int maxHealth;
    public int Health { get; set; }

    public static Action OnEnemyTookDamage;
    public static Action OnEnemyDeath;

    private void Start() {
        Health = maxHealth;
    }

    public void ModifyHealth(int health) {
        Health += health;
        if (health < 0) {
            OnEnemyTookDamage?.Invoke();
        }
        if (Health <= 0) {
            OnDeath();
        } else if (health > maxHealth) {
            Health = maxHealth;
        }

    }

    public void OnDeath() {
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
}
