using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private int xInput;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = player.InputHandler.NormalizedInputX;  // Determine player facing while walljumping

        player.InputHandler.UseJumpInput();

        player.JumpState.DecreaseAmountOfJumpsLeft();

        MakeWallJump(playerData.WallJumpVelocity, wallJumpDirection, playerData.WallJumpAngle);

        player.CheckIfShouldFlip(xInput);
    }


    public override void LogicUpdate()

    {
        base.LogicUpdate();

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);

        if (Time.time >= startTime + playerData.WallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    private void MakeWallJump(float velocity, int direction, Vector2 angle)
    {
        player.SetVelocityZero();
        player.Rigidbody.AddForce(velocity * direction * new Vector2(angle.x, angle.y * Mathf.Sign(direction)), ForceMode2D.Impulse);
    }

    /// <summary>
    ///  Calculate wall jump diraction base on if is touching wall
    /// </summary>
    /// <param name="isTouchingWall"></param>
    public void DetermineWallJumpDrection(bool isTouchingWall) => wallJumpDirection = isTouchingWall ? -player.FacingDirection : player.FacingDirection;

}
