using Assets.Scripts.Data.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    // Inputs
    protected int xInput;
    private bool jumpInput;
    private bool grabWallInput;

    private bool isGrounded;    // Need to know if player is grounded, obviously...
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();  // Check if player is grounded 
        isTouchingWall = player.CheckIsTouchingWall();  // Check if player is near facing wall
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();  // Reset jumps
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
        grabWallInput = player.InputHandler.GrabWallInput;

        // Chage states
        if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState); // Go to Jump state (ability)
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime(); // Start coyote timer
            stateMachine.ChangeState(player.InAirState);    // Go to InAir state
        }
        else if (grabWallInput && isTouchingWall)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
