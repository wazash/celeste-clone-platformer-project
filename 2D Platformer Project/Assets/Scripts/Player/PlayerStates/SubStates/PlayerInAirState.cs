using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Inputs
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabWallInput;
    private bool dashInput;

    // Chekers
    private bool isGrounded;
    private bool isJumping;
    private bool isTouchingWall;
    private bool oldIsTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIsGrounded();
        isTouchingWall = player.CheckIsTouchingWall();
        isTouchingWallBack = player.CheckIsTouchingWallBack();

        isTouchingLedge = player.CheckIfTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;

        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Get inputs
        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabWallInput = player.InputHandler.GrabWallInput;
        dashInput = player.InputHandler.DashInput;

        // Calculate 'coyote time'
        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        InAirMovement();
        LimitFallingSpeed();

        CheckJumpMultiplier();

        // Change states
        if (isGrounded && player.CurrentVelocity.y < playerData.MinGroundedVelocityY)    // change to some of grounded state
        {
            if (xInput == 0)
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.MoveState);
        }
        else if(isTouchingWall && !isTouchingLedge && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))    // change to wall jump ability state
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIsTouchingWall();  // prevent desync between checking isTouchingWall in fixeedUpdate and normal Update
            player.WallJumpState.DetermineWallJumpDrection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())   // change to jump ability state
        {
            coyoteTime = false;
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isTouchingWall) // change to touching wall state
        {
            if (grabWallInput)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if(xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && player.InputHandler.DashDirectionInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    /// <summary>
    /// If jumping, decrease jump vel by factor if jump button is released
    /// </summary>
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.JumpVelocityReductionFactor);
                SetIsJumping(false);
            }
            else if (player.CurrentVelocity.y < 0f)
            {
                SetIsJumping(false);
            }
        }
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
            StopCoyoteTime();
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    /// <summary>
    /// Initialize coyote timer
    /// </summary>
    public void StartCoyoteTime() => coyoteTime = true;
    public void StopCoyoteTime() => coyoteTime = false;


    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startTime + playerData.CoyoteTime)
        {
            StopWallJumpCoyoteTime();
            //player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    public void StartWallJumpCoyoteTime() => wallJumpCoyoteTime = true;
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;


    public void SetIsJumping(bool value) => isJumping = value;

    public void LimitFallingSpeed()
    {
        if(player.CurrentVelocity.y < -playerData.MaxFaliingSpeed)
        {
            player.SetVelocityY(-playerData.MaxFaliingSpeed);
        }
    }
}
