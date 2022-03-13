using Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class Runing : Grounded
    {
        private const string NAME = "Runing";

        public Runing(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        #region Overrides Methods
        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (input.x == 0)
            {
                stateMachine.ChangeState(sm.IdlingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            RunWhileGrounded();
        }
        #endregion

        #region Own Methods
        private void RunWhileGrounded()
        {
            sm.Rigidbody.velocity = new Vector2(sm.PlayerData.Speed * input.x, sm.Rigidbody.velocity.y);
        }

        #endregion
    }
}
