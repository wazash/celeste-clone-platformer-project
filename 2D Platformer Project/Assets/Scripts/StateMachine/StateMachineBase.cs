using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        private StateBase currentState;

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

            currentState = newState;
            currentState?.Enter();
        }

        protected virtual StateBase GetInitialState()
        {
            return null;
        }
    } 
}
