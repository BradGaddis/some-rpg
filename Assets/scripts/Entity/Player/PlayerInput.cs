using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float moveHorizontal;
    float moveVertical;

    private void Update() {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    public void HandleMovement(float runSpeed, PlayerStateMachine currentState, Rigidbody2D rb, bool lockedMovement) {
         if (moveHorizontal != 0 || moveVertical != 0)
        {
            currentState.ChangeState(PlayerState.Running);
        }
        else
        {
            currentState.ChangeState(PlayerState.Idle);
        }
        // lock diagonal movement
        if (moveHorizontal != 0 && moveVertical != 0 && lockedMovement)
        {
            moveHorizontal = 0;
        }
        rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * runSpeed;
    }

    public void HandleAttack(PlayerAnimationHandler playerAnimationHandler) {
        if (Input.GetKeyDown(KeyCode.V)) {
            playerAnimationHandler.StartForwardPunchAttack();
        }
    }

    public Vector3 GetMoveDirection() {
        return new Vector3(moveHorizontal, moveVertical, 0);
    }

    public Vector3 GetMoveDirectionNormalized() {
        return new Vector3(moveHorizontal, moveVertical, 0).normalized;
    }

    public bool IsMoving() {
        return moveHorizontal != 0 || moveVertical != 0;
    }

}
