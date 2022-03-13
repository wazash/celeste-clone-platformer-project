using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class Falling : InAir
    {
        private const string NAME = "Falling";

        public Falling(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

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

            if(sm.Rigidbody.velocity.y > 0)
            {
                stateMachine.ChangeState(sm.RaisingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            MoveWhileFalling();
        }

        private void MoveWhileFalling()
        {
            sm.Rigidbody.velocity = new Vector2(sm.PlayerData.Speed * input.x, sm.Rigidbody.velocity.y);
        }
    }
}
