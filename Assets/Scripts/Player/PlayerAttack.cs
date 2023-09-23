using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float delay;
    [SerializeField] float range;
    [SerializeField] float radius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackLocation;

    Vector2 attackDirection;

    bool inAttack;

    void Awake() {
        inAttack = false;
        InputHandler.OnBPressed += Handle_B_Pressed;
    }

    void Handle_B_Pressed() {
        if (!inAttack) { DoAttack(); }
    }

    void DoAttack() {
        StartCoroutine(CheckEnemyInRange());
        inAttack = false;
    }

    void Update() {
        attackDirection = PlayerAnimationState.Direction;
    }

    IEnumerator CheckEnemyInRange() {
        inAttack = true;
        attackDirection = PlayerAnimationState.Direction;
        RaycastHit2D hit = Physics2D.Raycast(attackLocation.position * radius, attackDirection * range, radius, enemyLayer);
        if (hit.collider != null) {
            Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
            hitEnemy.ModifyHealth(dmg * -1);
        }
        yield return new WaitForSeconds(delay);
    }
}
