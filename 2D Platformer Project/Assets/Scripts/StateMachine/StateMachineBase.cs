using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        public StateBase currentState;
        public StateBase previousState;

        protected virtual void Start()
        {
            currentState = GetInitialState();

            currentState?.Enter();
        }

        protected virtual void Update()
        {
            currentState?.UpdateLogic();
        }

        protected virtual void FixedUpdate()
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
