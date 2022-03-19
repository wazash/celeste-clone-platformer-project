using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    /// <summary>
    /// A substate of the InAir state. Player is raising
    /// </summary>
    public class Raising : InAir
    {
        private const string NAME = "Raising";  // State AND animation name

        public Raising(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);     // Play Raising animation from player Aniomator
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            #region Logic

            if (!Input.GetButton(sm.PlayerData.JumpAxis.ToString()))
            {
                sm.Rigidbody.velocity = new Vector2(sm.Rigidbody.velocity.x, sm.Rigidbody.velocity.y * sm.PlayerData.CancelJumpFactor);
            }

            #endregion

            #region Chage State

            if (sm.Rigidbody.velocity.y < 0)
            {
                stateMachine.ChangeState(sm.FallingState);  // Change state to Falling if vertical velocity is directly changed into negative 
            }

            #endregion
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            if(sm.CanPlayerControll)
                MoveWhileRaising();     // Move player while raising (seperate from falling movement)
        }

        #region Own Methods

        /// <summary>
        /// Allow move player while raising
        /// </summary>
        private void MoveWhileRaising()
        {
            sm.Rigidbody.velocity = new Vector2(sm.PlayerData.Speed * input.x, sm.Rigidbody.velocity.y);
        } 

        #endregion

    }
}
