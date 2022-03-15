using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the WallGrabbed state. Player is climbing up on corner wall
    /// </summary>
    public class WallGrabCornerClimbing : WallGrabbed
    {
        private const string NAME = "WallGrabCornerClimbing";   // State AND animation name

        public WallGrabCornerClimbing(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Rigidbody.gravityScale = 0;              // Set player gravity at 0 when enter state preventing falling 
            sm.Rigidbody.velocity = Vector2.zero;       // Set player velocity at 0 when enter state preventing moving

            sm.Animator.Play(NAME);     // Play WallGrabCornerClimbing animation from player Aniomator
        }

        public override void Exit()
        {
            base.Exit();

            sm.Rigidbody.gravityScale = sm.PlayerData.DefaultGravityScale;  // Set player gravity at default after exit state
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic

            #endregion

            #region Chage State

            if (sm.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                // Change character position after animation
                ChagePositionAfterEndAnimation(); // Causes multiple execute Enter()?? Don't know why the hell... But does not disturb

                stateMachine.ChangeState(sm.IdlingState);
            } 

            #endregion

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }


        #region Own Methods

        /// <summary>
        /// Set player position after end climbing animation. Animation doesn't changing player position, only sprite
        /// </summary>
        private void ChagePositionAfterEndAnimation()
        {
            sm.transform.position = new Vector2(sm.transform.position.x + (sm.PlayerData.ChangePositionOffset.x * sm.transform.localScale.x), sm.transform.position.y + sm.PlayerData.ChangePositionOffset.y);
        } 

        #endregion
    }
}
