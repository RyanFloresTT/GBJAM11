using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float delay;
    [SerializeField] float range;
    [SerializeField] float radius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackLocation;

    float nextHitTime;
    Vector2 attackDirection;

    public static Action OnPlayerAttack;

    void Awake() {
        nextHitTime = 0;
        InputHandler.OnBPressed += Handle_B_Pressed;
    }

    void Handle_B_Pressed() {
        if (Time.time > nextHitTime) { DoAttack(); }
    }

    void DoAttack() {
        OnPlayerAttack?.Invoke();
        StartCoroutine(CheckEnemyInRange());
        nextHitTime = Time.time + delay;
    }

    void Update() {
        attackDirection = PlayerAnimationState.Direction;
    }

    IEnumerator CheckEnemyInRange() {
        attackDirection = PlayerAnimationState.Direction;
        RaycastHit2D hit = Physics2D.CircleCast(attackLocation.position, radius ,attackDirection, range, enemyLayer);
        if (hit.collider != null) {
            Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
            hitEnemy.ModifyHealth(dmg * -1);
        }
        yield return new WaitForSeconds(delay);
    }
}
