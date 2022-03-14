using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class WallGrabCornerClimbing : WallGrabbed
    {
        private const string NAME = "WallGrabCornerClimbing";

        public WallGrabCornerClimbing(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Rigidbody.gravityScale = 0;
            sm.Rigidbody.velocity = Vector2.zero;

            sm.Animator.Play(NAME);
        }

        public override void Exit()
        {
            base.Exit();
            sm.Rigidbody.gravityScale = sm.PlayerData.DefaultGravityScale;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (sm.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                // Change character position after animation
                ChagePositionAfterEndAnimation(); // Causes multiple execute Enter()?? Don't know why the hell... But does not disturb
                
                stateMachine.ChangeState(sm.IdlingState);
            }

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        private void ChagePositionAfterEndAnimation()
        {
            sm.transform.position = new Vector2(sm.transform.position.x + (sm.PlayerData.ChangePositionOffset.x * sm.transform.localScale.x), sm.transform.position.y + sm.PlayerData.ChangePositionOffset.y);
        }
    }
}
