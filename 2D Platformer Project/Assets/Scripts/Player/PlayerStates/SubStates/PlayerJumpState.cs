using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public int AmountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
        AmountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Make jump
        Jump();

        // Playe particles
        player.Particles.JumpingPS.Play();

        // Play sound
        EventsManager.OnPlayedSfxPlay.Invoke(playerData.PlayerSounds.JumpClip);

        player.InputHandler.UseJumpInput();
        player.InAirState.SetIsJumping(true);

        DecreaseAmountOfJumpsLeft();
        player.InAirState.StopCoyoteTime();

        // Jump is ability, need to be set true to mark jump is done 
        SetAbilityDone();
    }

    private void Jump()
    {
        //player.SetVelocityY(playerData.InitialJumpVelocity);
        player.Rigidbody.AddForce(playerData.InitialJumpVelocity * Vector2.up, ForceMode2D.Impulse);
    }

    public bool CanJump() => AmountOfJumpsLeft > 0;
    public void ResetAmountOfJumpsLeft() => AmountOfJumpsLeft = playerData.AmountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => AmountOfJumpsLeft--;
    public void IncreaseAmountOfJumpsLeft() => AmountOfJumpsLeft++;
}
