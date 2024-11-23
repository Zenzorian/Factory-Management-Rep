using Scripts.MyTools;
using Scripts.Services;
using System;

namespace Scripts.Infrastructure.States
{
    public class StatisticProcessorState : IState
    {     
        private readonly StateMachine _stateMachine;      
       
        private readonly ISaveloadDataService _saveLoadData;
        
        public StatisticProcessorState(StateMachine gameStateMachine, ISaveloadDataService saveLoadData)
        {
          _stateMachine = gameStateMachine;         
          _saveLoadData = saveLoadData;
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