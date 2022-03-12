using Assets.Scripts.StateMachine.PlayerStateMachine.PlayerStates.SuperStates;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class Idling : Grounded
    {
        private const string NAME = "Idling";

        public Idling(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        #region Overrides Methods
        public override void Enter()
        {
            base.Enter();
            DecelerateWhileIdling();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (input.x != 0)
            {
                stateMachine.ChangeState(sm.WalkingState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
        #endregion

        #region Own Methods
        private void DecelerateWhileIdling()
        {
            sm.Rigidbody.velocity = new Vector2(sm.Rigidbody.velocity.x / sm.PlayerData.DecelerationFactor, sm.Rigidbody.velocity.y);
        } 
        #endregion

    }
}
