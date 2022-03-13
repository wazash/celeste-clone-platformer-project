using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class Raising : InAir
    {
        private const string NAME = "Raising";

        public Raising(PlayerSM stateMachine) : base(stateMachine, NAME)
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

            if(sm.Rigidbody.velocity.y < Mathf.Epsilon)
            {
                stateMachine.ChangeState(sm.FallingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            MoveWhileRaising();
        }

        private void MoveWhileRaising()
        {
            sm.Rigidbody.velocity = new Vector2(sm.PlayerData.Speed * input.x, sm.Rigidbody.velocity.y);
        }

    }
}
