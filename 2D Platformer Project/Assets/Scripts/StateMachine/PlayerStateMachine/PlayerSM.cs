using Assets.Scripts.Data;
using Assets.Scripts.StateMachine.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Scripts.StateMachine.PlayerStateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerSM : StateMachineBase
    {
        [field:SerializeField]
        public Rigidbody2D Rigidbody { get; private set; }
        [field:SerializeField]
        public PlayerData PlayerData { get; private set; }

        public Idling IdlingState { get; private set; }
        public Walking WalkingState { get; private set; }

        private void Awake()
        {
            IdlingState = new Idling(this);
            WalkingState = new Walking(this);
        }

        protected override StateBase GetInitialState()
        {
            return IdlingState;
        }
    }
}
