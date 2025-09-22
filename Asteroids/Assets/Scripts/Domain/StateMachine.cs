using System;
using System.Collections.Generic;
using Domain.Properties;

namespace Domain
{
    public class StateMachine
    {
        private IEnterable _enterState;
        private IExitable _exitState;
        private ICanUpdate _updateState;
        private ICanFixedUpdate _fixedUpdateState;
        
        protected object CurrentState;
        protected Dictionary<Type, object> States;
        
        public void EnterIn<TState>() 
            where TState : class
        {
            if (States.TryGetValue(typeof(TState), out var state))
            {
                _exitState?.Exit();

                CurrentState = state;
                _enterState = state as IEnterable;
                _exitState = state as IExitable;
                _updateState = state as ICanUpdate;
                _fixedUpdateState = state as ICanFixedUpdate;
                
                _enterState?.Enter();
            }
        }

        public void UpdateSystem()
        {
            _updateState?.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            _fixedUpdateState?.FixedUpdateSystem();
        }
    }
}
