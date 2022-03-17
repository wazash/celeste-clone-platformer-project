using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the InAir state. Player is falling
    /// </summary>
    public class Falling : InAir
    {
        private const string NAME = "Falling";  // State AND animation name

        public Falling(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);     // Play Falling animation from player Animator
        }

        public override void Exit()
        {
            base.Exit();

            sm.ExitVelocityY = sm.Rigidbody.velocity.y; // Save exit vertical velocity
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic

            // Reset buffer timer after pressed jump button
            if (Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
                sm.JumpBufferTimer = sm.PlayerData.JumpBufferTime;

            // Make Coyote Jump
            sm.CoyoteJumpTimer -= Time.deltaTime;
            if (sm.CoyoteJumpTimer > 0 && sm.previousState.Name == sm.RuningState.Name)
                CoyoteJump();

            #endregion

            #region Chage State

            // Change state to Raising if vertical velocity is directly changed into positive 
            if (sm.Rigidbody.velocity.y > 0)
            {
                stateMachine.ChangeState(sm.RaisingState);  
            } 

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            if(sm.Rigidbody.velocity.y < sm.PlayerData.MinVelocityY)
                sm.Rigidbody.velocity = new Vector2(sm.Rigidbody.velocity.x, sm.PlayerData.MinVelocityY);

            // Move player while falling (seperate from raising movement)
            if(sm.CanPlayerControll)
                MoveWhileFalling(); 
        }

        #region Own Methods

        /// <summary>
        /// Allow move player while falling
        /// </summary>
        /// 
        private void MoveWhileFalling()
        {
            sm.Rigidbody.velocity = new Vector2(sm.PlayerData.Speed * input.x, sm.Rigidbody.velocity.y);
        }

        /// <summary>
        /// Allow player jump short time after leaving grounded state, only if running
        /// </summary>
        private void CoyoteJump()
        {
            if (Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
            {
                sm.CoyoteJumpTimer = 0; // Zeroing timer preventing multiple jumps
                sm.Rigidbody.velocity = Vector2.zero; // Zeroing player velocity before coyote jump
                //sm.CoyoteJumpDust.Play();   // Play Coyote Jumping Dust while player pressed Jump Button
                sm.Rigidbody.AddForce(Vector2.up * sm.PlayerData.JumpForce, ForceMode2D.Impulse);   // Add vertical force for make player jump
            }
        }

        #endregion
    }
}