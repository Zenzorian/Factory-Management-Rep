using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class TableProcessorState : IPayloadedState<TableProcessorStateData>
    {
        private readonly StateMachine _stateMachine;

        private readonly ISaveloadDataService _saveLoadData;
        private readonly ITableProcessorService _tableProcessorService;
        private readonly GlobalUIElements _globalUIElements;

        private ChoiceOfCategoryStateData _categoryData;
        public TableProcessorState(StateMachine gameStateMachine, ISaveloadDataService saveLoadData, ITableProcessorService tableProcessorService, GlobalUIElements globalUIElements)
        {
            _stateMachine = gameStateMachine;
            _saveLoadData = saveLoadData;
            _tableProcessorService = tableProcessorService;
            _globalUIElements = globalUIElements;
        }

        public void Enter(TableProcessorStateData tableProcessorStateData)
        {
            _categoryData = tableProcessorStateData.choiceOfCategoryStateData;

            _globalUIElements.backButton.onClick.AddListener(Back);
            _globalUIElements.addationButton.onClick.AddListener(Addation);
                        

            _tableProcessorService.SetTableData(tableProcessorStateData.selectedListOfTableItems);
        }
        public void Exit()
        {
            _tableProcessorService.CloseTable();

            _globalUIElements.backButton.onClick.RemoveListener(Back);
            _globalUIElements.addationButton.onClick.RemoveListener(Addation);
        }
        private void Addation()
        {

        }

        private void Back()
        {
            _stateMachine.Enter<ChoiceOfCategoryState, ChoiceOfCategoryStateData>(_categoryData);           
        }
    }
    
    public struct TableProcessorStateData
    {
        public ChoiceOfCategoryStateData choiceOfCategoryStateData;
        public List<TableItem> selectedListOfTableItems;
    }
}