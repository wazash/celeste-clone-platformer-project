using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates
{
    public class InAir : StateBase
    {
        protected PlayerSM sm;

        protected Vector2 input;

        public InAir(PlayerSM stateMachine, string name) : base(stateMachine, name)
        {
            sm = stateMachine;
        }

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

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            sm.FlipDirection(input);

            if (sm.CheckGrounded())
            {
                if(input.x == 0)
                    stateMachine.ChangeState(sm.IdlingState);
                if(input.x != 0)
                    stateMachine.ChangeState(sm.RuningState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

    }
}
