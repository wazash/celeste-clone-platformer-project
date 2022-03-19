using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the WallGrabbed state. Player is sliding down on wall
    /// </summary>
    public class WallGrabSliding : WallGrabbed
    {
        private const string NAME = "WallGrabSliding";  // State AND animation name
        public WallGrabSliding(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);     // Play WallGrabSliding animation from player Aniomator

            //  Set player gravity to default gravity multiply by wall sliding factor.
            // Thats mean sliding will be faster the higher value of factor is set in PlayerDataSO.
            if(sm.CanPlayerControll)
            {
                sm.Rigidbody.gravityScale = sm.PlayerData.DefaultGravityScale * sm.PlayerData.SlidingGravityFactor;
                sm.Rigidbody.velocity = Vector2.zero;
            }
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

            //if (input.y == 0)
            //{
            //    stateMachine.ChangeState(sm.WallGrabIdlingState);   // Change state into idling on wall when vertical input is 0
            //}

            //if (input.y > 1)
            //{
            //    stateMachine.ChangeState(sm.WallGrabClimbingState); // Change state into idling on wall when vertical input is egative
            //} 

            if (Input.GetButton(sm.PlayerData.GrabWallAxis.ToString()) && input.y >= 0)
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);
            }

            if(sm.PlayerData.IsFacingRight && input.x < 0 || !sm.PlayerData.IsFacingRight && input.x > 0 || input.x == 0 && !Input.anyKey)
            {
                stateMachine.ChangeState(sm.FallingState);
            }

            if (sm.CheckGrounded())
            {
                stateMachine.ChangeState(sm.IdlingState);
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
