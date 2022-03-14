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

            // Input
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Flipping character
            sm.FlipDirection(input);

            // Change states conditions
            if (sm.CheckGrounded())
            {
                if(input.x == 0)
                    stateMachine.ChangeState(sm.IdlingState);
                if(input.x != 0)
                    stateMachine.ChangeState(sm.RuningState);
            }

            if (sm.CheckCanGrabWall() && Input.GetButton(sm.PlayerData.GrabWallAxis.ToString()))
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);
            }

            if (sm.CheckCanCornerGrab() && Input.GetButton(sm.PlayerData.JumpAxis.ToString()) && (Mathf.Abs(input.x) > 0 || input.y > 0))
            {
                stateMachine.ChangeState(sm.WallGrabCornerClimbingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

    }
}
