using Scripts.Services;
using System;

namespace Scripts.Infrastructure.States
{
    public class StatisticProcessorState : IState
    {     
        private readonly StateMachine _stateMachine;
       
        public StatisticProcessorState(StateMachine gameStateMachine)
        {
          _stateMachine = gameStateMachine;              
        }

        public void Enter()
        {
           
        }

        private void LoadDataOrInitNew()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        { 
        
        }

        private void OnLoaded()
        {  
          _stateMachine.Enter<MainMenuState>();
        }  
  }
}