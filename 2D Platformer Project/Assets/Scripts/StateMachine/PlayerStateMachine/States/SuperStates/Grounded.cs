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

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            sm.FlipDirection(input);

            if (!sm.CheckGrounded())
            {
                if(sm.Rigidbody.velocity.y > 0)
                    stateMachine.ChangeState(sm.RaisingState);
                if (sm.Rigidbody.velocity.y < 0)
                    stateMachine.ChangeState(sm.FallingState);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                sm.Rigidbody.AddForce(Vector2.up * sm.PlayerData.JumpForce, ForceMode2D.Impulse);
            }

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();


        }
        #endregion

        #region Own Methods

        #endregion
    }
}
