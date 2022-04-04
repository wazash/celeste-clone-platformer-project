using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float lastDashTime;
    private float freezeCurrentDuration;
    private float dashCurrentDuration;

    private bool canDash;
    private bool isDashing;
    private bool isFreezing;

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

        player.SetVelocityZero();

        canDash = false;

        player.InputHandler.UseDashInput();
        player.JumpState.DecreaseAmountOfJumpsLeft();

        isFreezing = true;
        isDashing = false;

        dashCurrentDuration = 0;
        freezeCurrentDuration = 0;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));

        // Short freeze before actual dash - time to set/change direction
        if (isFreezing)
        {
            freezeCurrentDuration += Time.fixedDeltaTime;
            DetermineDashDirection();

            if (freezeCurrentDuration >= playerData.BeforeDashFreezeTime)
            {
                isFreezing = false;
                isDashing = true;
            }
        }

        // Dash - apply force with specific direction
        if (isDashing)
        {
            dashCurrentDuration += Time.fixedDeltaTime;
            Dash();

            if (dashCurrentDuration >= playerData.DashTime)
            {
                isDashing = false;
                lastDashTime = Time.time;
                ApplyEndDashGravity();
                SetAbilityDone();
            }
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
