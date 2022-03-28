using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float startDash;
    private float lastDashTime;

    private bool canDash;
    private bool isDashing = false;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    #region Override Methods
    public override void Enter()
    {
        base.Enter();

        startDash = startTime + playerData.BeforeDashFreezeTime;
        player.SetVelocityZero();

        canDash = false;

        player.InputHandler.UseDashInput();
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isDashing)
        {
            DetermineDashDirection();
        }

        if (Time.time >= startTime + playerData.BeforeDashFreezeTime)
        {
            isDashing = true;

            player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));

            if (Time.time >= startDash + playerData.DashTime)
            {
                ApplyEndDashGravity();
                lastDashTime = Time.time;
                isDashing = false;
                SetAbilityDone();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isDashing)
        {
            Dash();
        }
    }
    #endregion

    #region Own Methods
    private void Dash()
    {
        player.Rigidbody.AddForce(playerData.DashVelocity * dashDirection, ForceMode2D.Impulse);
    }
    private void DetermineDashDirection()
    {
        dashDirectionInput = player.InputHandler.DashDirectionInput;
        if (dashDirectionInput != Vector2.zero)
        {
            dashDirection = dashDirectionInput.normalized;
        }
    }
    private void ApplyEndDashGravity()
    {
        if (player.CurrentVelocity.y > 0)
        {
            //player.SetVelocityY(player.CurrentVelocity.y * playerData.DashEndMultiplierY);
            player.Rigidbody.AddForce(player.CurrentVelocity.y * playerData.DashEndMultiplierY * Vector2.down, ForceMode2D.Impulse);
        }
    }
    public bool CheckIfCanDash() => canDash && Time.time > lastDashTime + playerData.DashCooldown;
    public void ResetCanDash() => canDash = true;
    #endregion
}
