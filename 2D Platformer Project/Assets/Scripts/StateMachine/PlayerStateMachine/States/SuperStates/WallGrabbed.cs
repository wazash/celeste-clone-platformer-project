using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates
{
    /// <summary>
    /// SuperState, player is grabbing wall
    /// </summary>
    public class WallGrabbed : StateBase
    {
        protected PlayerSM sm;      // Player State Machine
        protected Vector2 input;    // Input

        public WallGrabbed(PlayerSM stateMachine, string name) : base(stateMachine, name)
        {
            sm = stateMachine;  // Set sm as matching state machine
        }

        public override void Enter()
        {
            base.Enter();
            
            sm.ExitVelocityY = 0; // Reset exit Y velocity to prevent playing landing particles when enter this state directly form falling
        }

        public override void Exit()
        {
            base.Exit();

            if(sm.CanPlayerControll) // While dying, controlls are disabled, cant set gravity to default because it causes 'double-death' bug
                sm.Rigidbody.gravityScale = sm.PlayerData.DefaultGravityScale;  // When exit state set player gravity to default value
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic

            // Get input
            input = new Vector2(Input.GetAxisRaw(sm.PlayerData.HorizontalAxis.ToString()), Input.GetAxisRaw(sm.PlayerData.VerticalAxis.ToString()));

            if (Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
            {
                if (sm.CanPlayerControll)
                    WallJump(); // When jump button pressed make wall jump
            }
            #endregion

            #region Change State

            // Change states conditions
            if (Input.GetButtonUp(sm.PlayerData.GrabWallAxis.ToString())        // If grabwall button is released and ...
                && sm.currentState.Name != sm.WallGrabCornerClimbingState.Name  //... current substate is not WallGrabCornerClimbing or
                    || !sm.CheckCanGrabWall())                                  // ... is not in possition that allow grab wall
            {
                if (sm.CheckGrounded())
                {
                    stateMachine.ChangeState(sm.IdlingState);   // Change state to Idling if grounded
                }
                else
                {
                   stateMachine.ChangeState(sm.FallingState);  // Change state to Falling if in air
                }
            }



            if (sm.CheckCanCornerGrab() && (input.y > 0 || Mathf.Abs(input.x) > 0))
            {
                stateMachine.ChangeState(sm.WallGrabCornerClimbingState);   // Change state to climbing corner if is in proper position and proper input is get 
            } 

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }


        #region Own Methods

        /// <summary>
        /// Allow player to wall jump
        /// </summary>
        private void WallJump()
        {
            #region jump up but broken
            //if (input.y >= 0 && input.x == 0)
            //{
            //    // If not pressed any input or just vertical positive then jump up near wall
            //    if (sm.transform.localScale.x < 0)
            //    {
            //        sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * Vector2.right, ForceMode2D.Impulse);
            //        sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * sm.PlayerData.WallJumpUpMiltiplier * Vector2.one, ForceMode2D.Impulse);
            //    }
            //    if (sm.transform.localScale.x > 0)
            //    {
            //        sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * Vector2.left, ForceMode2D.Impulse);
            //        sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * sm.PlayerData.WallJumpUpMiltiplier * Vector2.one, ForceMode2D.Impulse);
            //    }
            //} 
            #endregion

            if (sm.transform.localScale.x < 0)
            {
                // If grabbed left wall then jump into right side
                if(input.x > 0)
                {
                    sm.Rigidbody.velocity = Vector2.zero; // Zeroed velocity before jump for prevent incorrect force
                    sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * Vector2.one, ForceMode2D.Impulse);
                }
            }
            else
            {
                // If grabbed right wall then jump into left side
                if(input.x < 0)
                {
                    sm.Rigidbody.velocity = Vector2.zero; // Zeroed velocity before jump for prevent incorrect force
                    sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * new Vector2(-1, 1), ForceMode2D.Impulse);
                }
            }
        } 

        #endregion
    }
}
