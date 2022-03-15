using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates
{
    /// <summary>
    /// SuperState, player is in ground
    /// </summary>
    public class Grounded : StateBase
    {
        protected PlayerSM sm;          // Player State Machine
        protected Vector2 input;        // Input

        public Grounded(PlayerSM stateMachine, string name) : base(stateMachine, name)
        {
            sm = stateMachine; // Set sm as matching state machine
        }

        #region Overrides Methods
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
            input = new Vector2(Input.GetAxisRaw(sm.PlayerData.HorizontalAxis.ToString()), Input.GetAxisRaw(sm.PlayerData.VerticalAxis.ToString()));

            // Flip player while in any of grounded state
            sm.FlipDirection(input);

            // Make jump
            JumpWhileGrounded(); 
            #endregion

            #region Chage State
            if (!sm.CheckGrounded())
            {
                if (sm.Rigidbody.velocity.y > 0)
                    stateMachine.ChangeState(sm.RaisingState);  // Set state to Raising if player's vertical velocity is positive
                if (sm.Rigidbody.velocity.y < 0)
                    stateMachine.ChangeState(sm.FallingState);  // Set state to Falling if player's vertical velocity is negative
            }

            if (sm.CheckCanGrabWall() && Input.GetButton(sm.PlayerData.GrabWallAxis.ToString()))
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);   // If can grab wall and relevant button is pressed then set state to WallGrabIdlingState
            } 
            #endregion

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
        #endregion

        #region Own Methods

        private void JumpWhileGrounded()
        {
            if (Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
            {
                sm.JumpedDust.Play();   // Play Jumping Dust while player pressed Jump Button
                sm.Rigidbody.AddForce(Vector2.up * sm.PlayerData.JumpForce, ForceMode2D.Impulse);   // Add vertical force for make player jump
            }
        }
        #endregion
    }
}
