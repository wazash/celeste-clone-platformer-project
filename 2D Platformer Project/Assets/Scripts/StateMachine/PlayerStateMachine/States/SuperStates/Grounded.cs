using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        } 
        #endregion
    }
}
