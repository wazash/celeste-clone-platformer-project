using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
        amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();

        // Make jump
        player.SetVelocityY(playerData.JumpVelocity);
        player.InAirState.SetIsJumping(true);

        // Jump is ability, need to be set true to mark jump is done 
        isAbilityDone = true;

        DecreaseAmountOfJumpsLeft();
    }

    public bool CanJump() => amountOfJumpsLeft > 0;
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.AmountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    public void IncreaseAmountOfJumpsLeft() => amountOfJumpsLeft++;
}
