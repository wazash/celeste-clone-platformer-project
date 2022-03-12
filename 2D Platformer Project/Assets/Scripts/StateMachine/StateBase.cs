using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public abstract class StateBase
    {
        public string Name { get; private set; }
        protected StateMachineBase stateMachine;

        protected StateBase(StateMachineBase stateMachine, string name)
        {
            this.stateMachine = stateMachine;
            Name = name;
        }

        public virtual void Enter() { Debug.Log($"{Name} from BaseState."); }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }
}