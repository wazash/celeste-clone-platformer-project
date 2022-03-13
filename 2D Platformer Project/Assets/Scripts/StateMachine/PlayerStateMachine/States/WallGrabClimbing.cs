using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class WallGrabClimbing : WallGrabbed
    {
        private const string NAME = "WallGrabClimbing";
        public WallGrabClimbing(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);

            sm.Rigidbody.gravityScale = -sm.PlayerData.DefaultGravityScale / sm.PlayerData.ClimbingGravityFactor;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if(input.y == 0)
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
    }
}
