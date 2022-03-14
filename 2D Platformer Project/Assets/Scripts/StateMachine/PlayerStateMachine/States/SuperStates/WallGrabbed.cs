using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates
{
    public class WallGrabbed : StateBase
    {
        protected PlayerSM sm;
        protected Vector2 input;

        public WallGrabbed(PlayerSM stateMachine, string name) : base(stateMachine, name)
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

            //sm.Rigidbody.velocity = lastVelocity;
            sm.Rigidbody.gravityScale = sm.PlayerData.DefaultGravityScale;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            input = new Vector2(Input.GetAxisRaw(sm.PlayerData.HorizontalAxis.ToString()), Input.GetAxisRaw(sm.PlayerData.VerticalAxis.ToString()));

            if (Input.GetButtonDown(sm.PlayerData.JumpAxis.ToString()))
            {
                WallJump();
            }

            // Change states conditions
            if (Input.GetButtonUp(sm.PlayerData.GrabWallAxis.ToString()) && sm.currentState.Name != sm.WallGrabCornerClimbingState.Name || !sm.CheckCanGrabWall())
            {
                if (sm.CheckGrounded())
                {
                    stateMachine.ChangeState(sm.IdlingState);
                }
                else
                {
                    stateMachine.ChangeState(sm.FallingState);
                }
            }

            if (sm.CheckCanCornerGrab() && input.y > 0)
            {
                stateMachine.ChangeState(sm.WallGrabCornerClimbingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();


        }

        // ========= OWN METHODS =========

        private void WallJump()
        {
            sm.Rigidbody.velocity = Vector2.zero;

            if(input.y >= 0 && input.x == 0)
            {
                sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * sm.PlayerData.WallJumpUpMiltiplier * Vector2.one, ForceMode2D.Impulse);
            }

            if (sm.transform.localScale.x < 0)
            {
                sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * Vector2.one, ForceMode2D.Impulse);
            }
            else
            {
                sm.Rigidbody.AddForce(sm.PlayerData.JumpForce * new Vector2(-1, 1), ForceMode2D.Impulse);
            }
        }
    }
}
