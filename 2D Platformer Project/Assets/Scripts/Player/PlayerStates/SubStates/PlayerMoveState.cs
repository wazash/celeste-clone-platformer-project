using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    // ===== Overrides =====

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check if should flip
        player.CheckIfShouldFlip(xInput);

        // Move player
        Move();

        // Change state to IdleState
        if (xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    // ===== Own Methods =====
    private void Move()
    {
        player.SetVelocityX(playerData.MovementVelocity * xInput);
    }
}