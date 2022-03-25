using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{

    public bool CanDash { get; private set; }

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;


    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;

        player.InputHandler.UseDashInput();

        dashDirection = Vector2.right * player.FacingDirection;
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        if (player.CurrentVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.DashEndMultiplierY);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //player.Animator.SetFloat(AnimationName.InAir.ToString(), player.CurrentVelocity.y);

        if (!isExitingState)
        {
            dashDirectionInput = player.InputHandler.DashDirectionInput;
            

            if (dashDirectionInput != Vector2.zero)
            {
                dashDirection = dashDirectionInput.normalized;
            }

            player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
            player.Rigidbody.drag = playerData.DashDrag;

            player.SetVelocity(playerData.DashVelocity, dashDirection);

            if (Time.time >= startTime + playerData.DashTime)
            {
                player.Rigidbody.drag = 0f;
                isAbilityDone = true;
                lastDashTime = Time.time;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CheckIfCanDash() => CanDash && Time.time > lastDashTime + playerData.DashCooldown;

    public void ResetCanDash() => CanDash = true;
}
