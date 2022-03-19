using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the WallGrabbed state. Player is climbing up on wall
    /// </summary>
    public class WallGrabClimbing : WallGrabbed
    {
        private const string NAME = "WallGrabClimbing"; // State AND animation name
        public WallGrabClimbing(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);     // Play WallGrabClimbing animation from player Aniomator

            // Set player gravity to negative default gravity multiply by wall sliding factor.
            // Thats mean climbing will be faster the higher value of factor is set in PlayerDataSO.
            //sm.Rigidbody.gravityScale = -sm.PlayerData.DefaultGravityScale * sm.PlayerData.ClimbingGravityFactor;
            if(sm.CanPlayerControll)
                sm.Rigidbody.gravityScale = -sm.Rigidbody.gravityScale;
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

            if (input.y == 0)
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);
            }

            //if (input.y < 0)
            //{
            //    stateMachine.ChangeState(sm.WallGrabSlidingState);
            //} 

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            LimitClimbingSpeed();
        }

        public void LimitClimbingSpeed()
        {
            if (sm.Rigidbody.velocity.y > sm.PlayerData.ClimbingSpeed)
            {
                sm.Rigidbody.velocity = new Vector2(sm.Rigidbody.velocity.x, sm.PlayerData.ClimbingSpeed);
            }
        }
    }
}
