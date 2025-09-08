using System;
using System.Collections.Generic;
using Domain.Properties;

namespace Domain
{
    public class StateMachine
    {
        private ICanUpdate _updateState;
        private ICanFixedUpdate _fixedUpdateState;
        
        protected IState CurrentState;
        protected Dictionary<Type, IState> States;
        
        public void EnterIn<TState>() 
            where TState : IState
        {
            if (States.TryGetValue(typeof(TState), out IState state))
            {
                CurrentState?.Exit();
                
                CurrentState = state;
                _updateState = CurrentState as ICanUpdate;
                _fixedUpdateState = CurrentState as ICanFixedUpdate;
                
                CurrentState.Enter();
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
