using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    #region VARIABLES
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

    private int frames;
    #endregion

    #region CONSTRUCTOR
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }
    #endregion

    #region OVERRIDES
    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIsGrounded();  // Grounded
        isTouchingWall = player.CheckIsTouchingWall();  // Touching wall with front
        isTouchingWallBack = player.CheckIsTouchingWallBack();  // Touching wall with back

        isTouchingLedge = player.CheckIfTouchingLedge();    // Ledge detector is touching wall

        // Set player position when wall ray is touching wall and ledge ray is not touching
        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        // Initialize wall coyote jump 
        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
        frames = 0;
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log(frames);

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;

        isTouchingWall = false;
        isTouchingWallBack = false;

        player.Animator.SetFloat("yVelocity", 1f);  // Prevent bug with jumping animation
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        frames++;

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);    // control blend tree parameter

        GetInputs();

        LimitFallingSpeed();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        CheckJumpMultiplier();

        #region CHANGE STATE
        // change to some of GROUNDED state
        if (isGrounded && player.CurrentVelocity.y < playerData.MinGroundedVelocityY)
        {
            if (xInput == 0)
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.MoveState);
        }

        // change to LEDGE CLIMBING state
        if (isTouchingWall && !isTouchingLedge && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }

        // change to WALL JUMP ability state
        if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIsTouchingWall();  // prevent desync between checking isTouchingWall in fixeedUpdate and normal Update
            player.WallJumpState.DetermineWallJumpDrection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }

        // change to JUMP ability state
        if (jumpInput && player.JumpState.CanJump())
        {
            coyoteTime = false;
            stateMachine.ChangeState(player.JumpState);
        }

        // change to TOUCHING WALL state
        if (isTouchingWall)
        {
            if (grabWallInput)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }

        // change to DASH ability state
        if (dashInput && player.DashState.CheckIfCanDash() && player.InputHandler.DashDirectionInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.DashState);
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        ApplyGravity();
        InAirMovement();
    }
    #endregion

    #region OWN METHODS
    private void GetInputs()
    {
        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabWallInput = player.InputHandler.GrabWallInput;
        dashInput = player.InputHandler.DashInput;
    }

    /// <summary>
    /// Add gravity force to player while not touching ground
    /// </summary>
    private void ApplyGravity()
    {
        player.Rigidbody.AddForce(playerData.Gravity * Vector2.up, ForceMode2D.Force);
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

        float targetSpeed = playerData.MovementVelocity * xInput;

        float speedDifference = targetSpeed - player.CurrentVelocity.x;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * playerData.Acceleration, playerData.VelocityPower) * Mathf.Sign(speedDifference);

        player.Rigidbody.AddForce(movement * Vector2.right);

        //player.SetVelocityX(playerData.MovementVelocity * xInput);
    }

    /// <summary>
    /// Calculate Coyote Time
    /// </summary>
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.CoyoteTime)
        {
            StopCoyoteTime();
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StopCoyoteTime() => coyoteTime = false;


    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startTime + playerData.CoyoteTime)
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
        if (player.CurrentVelocity.y < -playerData.MaxFaliingSpeed)
        {
            player.SetVelocityY(-playerData.MaxFaliingSpeed);
        }
    }
} 
#endregion
