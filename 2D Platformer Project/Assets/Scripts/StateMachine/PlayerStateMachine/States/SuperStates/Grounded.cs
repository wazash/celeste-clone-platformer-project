using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates
{
    public class Grounded : StateBase
    {
        protected PlayerSM sm;
        protected Vector2 input;

        public Grounded(PlayerSM stateMachine, string name) : base(stateMachine, name)
        {
            sm = stateMachine;
        }

        #region Overrides Methods
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

            input = new Vector2(Input.GetAxisRaw(sm.PlayerData.HorizontalAxis.ToString()), Input.GetAxisRaw(sm.PlayerData.VerticalAxis.ToString()));

            sm.FlipDirection(input);

            if (!sm.CheckGrounded())
            {
                if(sm.Rigidbody.velocity.y > 0)
                    stateMachine.ChangeState(sm.RaisingState);
                if (sm.Rigidbody.velocity.y < 0)
                    stateMachine.ChangeState(sm.FallingState);
            }

            if (Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
            {
                sm.Rigidbody.AddForce(Vector2.up * sm.PlayerData.JumpForce, ForceMode2D.Impulse);
            }

            if(sm.CheckCanGrabWall() && Input.GetButton(sm.PlayerData.GrabWallAxis.ToString()))
            {
                stateMachine.ChangeState(sm.WallGrabIdlingState);
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
