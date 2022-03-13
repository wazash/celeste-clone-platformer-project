using Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class Idling : Grounded
    {
        private const string NAME = "Idling";   // State AND animation name

        public Idling(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        #region Overrides Methods
        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (sm.Rigidbody.velocity.x != 0)
            {
                stateMachine.ChangeState(sm.RuningState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            DecelerateWhileIdling(sm.PlayerData.DecelerationFactor, sm.PlayerData.MinVelocityX);
        }
        #endregion

        #region Own Methods
        private void DecelerateWhileIdling(float factor, float minVelocityX)
        {
            if(Mathf.Abs(sm.Rigidbody.velocity.x) > minVelocityX)
                sm.Rigidbody.velocity = new Vector2(sm.Rigidbody.velocity.x * factor, sm.Rigidbody.velocity.y);
            else 
                sm.Rigidbody.velocity = new Vector2(0, sm.Rigidbody.velocity.y);
        } 
        #endregion

    }
}
