using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool jumpInput;

    private bool isGrounded;
    private bool coyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // Get inputs
        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;

        // Calculate 'coyote time'
        CheckCoyoteTime();

        // Change states
        if(isGrounded && player.CurrentVelocity.y < playerData.MinGroundedVelocityY)    // change to some of grounded state
        {
            if(xInput == 0)
                stateMachine.ChangeState(player.IdleState);
            else 
                stateMachine.ChangeState(player.MoveState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else // allow moving in air
        {
            InAirMovement();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    /// <summary>
    /// Allows moving in air
    /// </summary>
    private void InAirMovement()
    {
        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(playerData.MovementVelocity * xInput);

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);    // control blend tree parameter
    }

    /// <summary>
    /// Calculate Coyote Time
    /// </summary>
    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.CoyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    /// <summary>
    /// Initialize coyote timer
    /// </summary>
    public void StartCoyoteTime() => coyoteTime = true;
}
