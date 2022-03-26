using Assets.Scripts.Data.Player;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected string animationBoolName;
    protected bool isExitingState;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animationBoolName, true); // Start playing animation
        startTime = Time.time;  // Start counting enter time
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationBoolName, false);  // Stop playing animation
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    /// <summary>
    /// Make some checks (e.x. IsGrounded), executed in Enter and PhysicsUpdate
    /// </summary>
    public virtual void DoChecks()
    {

    }
}
