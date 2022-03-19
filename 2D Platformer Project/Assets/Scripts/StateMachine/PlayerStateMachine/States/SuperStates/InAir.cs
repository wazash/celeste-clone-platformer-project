using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates
{
    /// <summary>
    /// SuperState, player is in air
    /// </summary>
    public class InAir : StateBase
    {
        protected PlayerSM sm;      // Player State Machine
        protected Vector2 input;    // Input

        public InAir(PlayerSM stateMachine, string name) : base(stateMachine, name)
        {
            sm = stateMachine;  // Set sm as matching state machine
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic
            // Get input
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Flipping character
            sm.FlipDirection(input); 
            #endregion

            #region Chage State
            // Change states conditions
            if (sm.CheckGrounded())
            {
                if (/*input.x == 0*/ sm.Rigidbody.velocity.x == 0)
                    stateMachine.ChangeState(sm.IdlingState);   // If grounded and horizontal veocity is equal to 0 set Idling state
                if (input.x != 0)
                    stateMachine.ChangeState(sm.RuningState);   // If grounded and horizontal veocity is not equal to 0 set Runing state
            }

            if (sm.CheckCanGrabWall() && Input.GetButton(sm.PlayerData.GrabWallAxis.ToString()))
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);   // If can grab wall and relevant button is pressed then set state to WallGrabIdlingState
            }

            if (sm.CheckCanCornerGrab() && Input.GetButton(sm.PlayerData.JumpAxis.ToString()) && (Mathf.Abs(input.x) > 0 || input.y > 0))
            {
                stateMachine.ChangeState(sm.WallGrabCornerClimbingState);   // If can grab wall and relevant button is pressed and relevant input is reached set state to WallGrabCornerClimbing
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
