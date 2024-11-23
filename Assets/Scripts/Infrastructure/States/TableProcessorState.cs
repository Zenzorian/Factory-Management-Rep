using Scripts.Services;
using System;

namespace Scripts.Infrastructure.States
{
    public class TableProcessorState : IState
    {     
        private readonly StateMachine _stateMachine;      
       
        private readonly ISaveloadDataService _saveLoadData;
        //private readonly ITableProcessorService _tableProcessorService;

        public TableProcessorState(StateMachine gameStateMachine, ISaveloadDataService saveLoadData)
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