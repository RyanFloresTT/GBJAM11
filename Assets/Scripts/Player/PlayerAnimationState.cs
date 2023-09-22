using UnityEngine;

public class PlayerAnimationState : MonoBehaviour {
    private enum AnimationState {
        IdleRight,
        IdleLeft,
        IdleUp,
        IdleDown,
    }

    [SerializeField] private Animator anim;
    [SerializeField] private MovesWithInput movement;

    private AnimationState currentState = AnimationState.IdleRight;

    private void OnEnable() {
        InputHandler.OnBPressed += Attack;
    }

    private void OnDisable() {
        InputHandler.OnBPressed -= Attack;
    }

    private void Update() {
        Vector2 direction = movement.CurrentDirection;

        AnimationState newState = GetStateFromDirection(direction);

        if (newState != currentState) {
            ChangeAnimationState(newState);
        }
    }

    private void Attack() {
        anim.SetTrigger("Attack");
    }

    public void ResetAttack() {
        anim.ResetTrigger("Attack");
    }

    private AnimationState GetStateFromDirection(Vector2 direction) {
        if (direction == Vector2.right) {
            return AnimationState.IdleRight;
        } else if (direction == Vector2.left) {
            return AnimationState.IdleLeft;
        } else if (direction == Vector2.up) {
            return AnimationState.IdleUp;
        } else if (direction == Vector2.down) {
            return AnimationState.IdleDown;
        }

        return currentState;
    }

    private void ChangeAnimationState(AnimationState state) {
        switch (state) {
            case AnimationState.IdleRight:
                anim.SetInteger("State", 0);
                FlipSprite(true);
                break;
            case AnimationState.IdleLeft:
                anim.SetInteger("State", 0);
                FlipSprite(false);
                break;
            case AnimationState.IdleUp:
                anim.SetInteger("State", 1);
                break;
            case AnimationState.IdleDown:
                anim.SetInteger("State", 2);
                break;
        }

        currentState = state;
    }

    private void FlipSprite(bool faceRight) {
        Vector3 localScale = transform.localScale;
        if (faceRight) {
            localScale.x = Mathf.Abs(localScale.x); // Face right
        } else {
            localScale.x = -Mathf.Abs(localScale.x); // Face left
        }
        transform.localScale = localScale;
    }
}
