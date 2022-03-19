using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    /// <summary>
    /// Base for all SuperStates.
    /// </summary>
    public abstract class StateBase
    {
        public string Name { get; private set; }
        protected StateMachineBase stateMachine;

        protected StateBase(StateMachineBase stateMachine, string name)
        {
            this.stateMachine = stateMachine;
            Name = name;
        }

        /// <summary>
        /// Executes the code once when entering the state
        /// </summary>
        public virtual void Enter() { Debug.Log(Name); }
        /// <summary>
        /// Executes the code every Update() (logic calculation)
        /// </summary>
        public virtual void UpdateLogic() { }
        /// <summary>
        /// Executes the code every FixedUpdate() (physic calculation)
        /// </summary>
        public virtual void UpdatePhysics() { }
        /// <summary>
        /// Executes the code once when exiting the state
        /// </summary>
        public virtual void Exit() { }
    }
}