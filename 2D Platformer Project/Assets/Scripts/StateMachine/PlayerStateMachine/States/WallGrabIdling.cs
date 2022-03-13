using Assets.Scripts.StateMachine.PlayerStateMachine.States.SuperStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine.States
{
    public class WallGrabIdling : WallGrabbed
    {
        private const string NAME = "WallGrabIdling";
        public WallGrabIdling(PlayerSM stateMachine) : base(stateMachine, NAME)
        {
        }

        public override void Enter()
        {
            base.Enter();

            sm.Animator.Play(NAME);

            sm.Rigidbody.velocity = Vector2.zero;
            sm.Rigidbody.gravityScale = 0;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if(input.y < 0)
            {
                stateMachine.ChangeState(sm.WallGrabSlidingState);
            }

            if(input.y > 0)
            {
                stateMachine.ChangeState(sm.WallGrabClimbingState);
            }

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }


    }
}
