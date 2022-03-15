using Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the Grounded state. Player is moving
    /// </summary>
    public class Runing : Grounded
    {
        private const string NAME = "Runing";   // State AND animation name

        public Runing(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        #region Overrides Methods
        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);     // Play Runing animation from player Aniomator
            sm.FootDustPS.Play();       // Start playing Foot Dust from player leg when enter state
        }

        public override void Exit()
        {
            base.Exit();

            sm.FootDustPS.Stop();       // Stop playing Foot Dust from player leg when exit state
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic

            #endregion

            #region Change States

            if (/*sm.Rigidbody.velocity.x == 0*/ input.x == 0)
            {
                stateMachine.ChangeState(sm.IdlingState);   // Chage state if horizontal input is 0 
            } 

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            RunWhileGrounded();

        }
        #endregion


        #region Own Methods

        /// <summary>
        /// Allows player to move while grounded
        /// </summary>
        protected void RunWhileGrounded()
        {
            sm.Rigidbody.velocity = new Vector2(sm.PlayerData.Speed * input.x, sm.Rigidbody.velocity.y);
        }

        #endregion
    }
}
