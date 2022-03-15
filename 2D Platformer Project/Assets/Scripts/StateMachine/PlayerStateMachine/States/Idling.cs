using Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the Grounded state. Player is standing in place
    /// </summary>
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

            sm.Animator.Play(NAME);     // Play Idling animation from player Animator
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic

            #endregion

            #region Change State

            // Changed for test, if will some issues with moving then change back
            if (/*sm.Rigidbody.velocity.x != 0 &&*/ input.x != 0)
            {
                stateMachine.ChangeState(sm.RuningState);   // Chage state if player is moving (non-zero horizontal velocity || input)
            } 

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            DecelerateWhileIdling(sm.PlayerData.DecelerationFactor, sm.PlayerData.MinVelocityX);
        }
        #endregion

        #region Own Methods

        /// <summary>
        /// Decelerate player velocity
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="minVelocityX"></param>
        private void DecelerateWhileIdling(float factor, float minVelocityX)
        {
            if(Mathf.Abs(sm.Rigidbody.velocity.x) > minVelocityX)
                // If horizontal velocity is greater then minVelocityX then smooth decrease current velocity multiply it by factor
                sm.Rigidbody.velocity = new Vector2(sm.Rigidbody.velocity.x * factor, sm.Rigidbody.velocity.y);
            else
                // If horizontal velocity is less then minVelocityX then set current velocity to 0
                sm.Rigidbody.velocity = new Vector2(0, sm.Rigidbody.velocity.y);
        } 

        #endregion

    }
}
