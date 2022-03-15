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

            sm.Animator.Play(NAME);     // Play Falling animation from player Aniomator
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

            #region Chage State

            if (sm.Rigidbody.velocity.y > 0)
            {
                stateMachine.ChangeState(sm.RaisingState);  // Change state to Raising if vertical velocity is directly changed into positive 
            } 

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            MoveWhileFalling(); // Move player while falling (seperate from raising movement)
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

        #endregion
    }
}