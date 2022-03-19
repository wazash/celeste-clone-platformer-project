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

            if(sm.ExitVelocityY < sm.PlayerData.FallingSpeedRequiredToPlayLandingParticle)
            {
                PlayLandingParticleEffects();   // Playing landing particle effects if condition is fulfilled
                sm.ExitVelocityY = 0;
            }
     
            // Reset coyote timer when grounded
            sm.CoyoteJumpTimer = sm.PlayerData.CoyoteTime;
        }

        public override void Exit()
        {
            base.Exit();

            input = Vector2.zero;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic 

            // Get input is controlling is avaliable
            if (sm.CanPlayerControll)
                input = new Vector2(Input.GetAxisRaw(sm.PlayerData.HorizontalAxis.ToString()), Input.GetAxisRaw(sm.PlayerData.VerticalAxis.ToString()));

            // Flip player while in any of grounded state
            sm.FlipDirection(input);

            //// Make jump when buffer timer is above zero or jump button iis pressed
            if(sm.JumpBufferTimer > Mathf.Epsilon || Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
            {
                JumpWhileGrounded();    // Jump
                //sm.JumpBufferTimer = 0; // Zeroing buffer timer
            }


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
            sm.JumpedDust.Play();   // Play Jumping Dust while player pressed Jump Button
            sm.Rigidbody.velocity = Vector2.zero;   // Reset player velocity before jump
            sm.Rigidbody.AddForce(Vector2.up * sm.PlayerData.JumpForce, ForceMode2D.Impulse);   // Add vertical force for make player jump  
        }

        /// <summary>
        /// Play landing particles if falling speed is enough
        /// </summary>
        private void PlayLandingParticleEffects()
        {
            sm.LandingDust.Play();
        }

        #endregion
    }
}
