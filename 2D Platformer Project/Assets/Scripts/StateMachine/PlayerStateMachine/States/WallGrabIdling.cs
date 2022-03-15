using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the WallGrabbed state. Player is idling on wall
    /// </summary>
    public class WallGrabIdling : WallGrabbed
    {
        private const string NAME = "WallGrabIdling";   // State AND animation name
        public WallGrabIdling(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);     // Play WallGrabIdling animation from player Aniomator

            sm.Rigidbody.velocity = Vector2.zero;   // Set player velocity to zero when enter to state
            sm.Rigidbody.gravityScale = 0;          // Set player gravity to zero when enter to state
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

            if (input.y < 0)
            {
                stateMachine.ChangeState(sm.WallGrabSlidingState);  // Change state to wall sliding if vertical input is lesser then 0
            }

            if (input.y > 0)
            {
                stateMachine.ChangeState(sm.WallGrabClimbingState); // Change state to wall climbing if vertical input is greater then 0
            } 

            #endregion

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        #region Own Methods

        #endregion
    }
}
