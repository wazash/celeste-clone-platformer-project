using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    // ===== Overrides =====

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check if should flip
        //player.CheckIfShouldFlip(xInput);

        // Move player
        //Move();

        // Change state to IdleState
        if (xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //player.Move(xInput);
        Move();

    }

    // ===== Own Methods =====
    private void Move()
    {
        //player.SetVelocityX(playerData.MovementVelocity * xInput);

        float targetSpeed = playerData.MovementVelocity * xInput;

        float speedDifference = targetSpeed - player.CurrentVelocity.x;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * playerData.Acceleration, playerData.VelocityPower) * Mathf.Sign(speedDifference);

        player.Rigidbody.AddForce(movement * Vector2.right);
    }
}