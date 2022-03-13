using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        public StateBase currentState;
        public StateBase previousState;

        private void Start()
        {
            currentState = GetInitialState();

            currentState?.Enter();
        }

        private void Update()
        {
            currentState?.UpdateLogic();
        }

        private void FixedUpdate()
        {
            currentState?.UpdatePhysics();
        }

        public void ChangeState(StateBase newState)
        {
            currentState?.Exit();

            previousState = currentState;
            currentState = newState;
            currentState?.Enter();
        }

        protected virtual StateBase GetInitialState()
        {
            return null;
        }
    }
}
