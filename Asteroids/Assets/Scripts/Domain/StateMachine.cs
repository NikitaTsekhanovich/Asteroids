using System;
using System.Collections.Generic;
using Domain.Properties;

namespace Domain
{
    public class StateMachine
    {
        private ICanUpdate _updateState;
        private ICanFixedUpdate _fixedUpdateState;
        private IState _currentState;
        
        protected Dictionary<Type, IState> States;
        
        public void EnterIn<TState>() 
            where TState : IState
        {
            if (States.TryGetValue(typeof(TState), out IState state))
            {
                _currentState?.Exit();
                
                _currentState = state;
                _updateState = _currentState as ICanUpdate;
                _fixedUpdateState = _currentState as ICanFixedUpdate;
                
                _currentState.Enter();
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
