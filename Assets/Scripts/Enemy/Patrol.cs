using UnityEngine;

public class Patrol : MonoBehaviour {
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float moveSpeed = 2.0f;
    int idx = 0;
    Vector3 moveDir;

    void Start() {
        if (patrolPoints.Length > 0) {
            transform.position = patrolPoints[0].position;
            moveDir = GetNewDirection();
        } else {
            Debug.LogError("No patrol points assigned.");
        }
    }

    void Update() {
        MoveToNextPoint();
    }

    void MoveToNextPoint() {
        if (Vector3.Distance(transform.position, patrolPoints[idx].position) < 0.1f) {
            idx = (idx + 1) % patrolPoints.Length;
            moveDir = GetNewDirection();
        }

        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    Vector3 GetNewDirection() {
        return (patrolPoints[idx].position - transform.position).normalized;
    }
}
