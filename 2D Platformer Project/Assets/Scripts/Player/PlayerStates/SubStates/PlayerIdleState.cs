using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    // ===== Overrides =====

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Change state to MoveState
        if (xInput != 0 & !isExitingState)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}
