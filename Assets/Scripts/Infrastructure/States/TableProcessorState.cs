using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class TableProcessorState : IPayloadedState<TableProcessorStateData>
    {
        private readonly StateMachine _stateMachine;
        
        private readonly ITableProcessorService _tableProcessorService;
        private readonly IItemAddationService _addationService;
        private readonly GlobalUIElements _globalUIElements;

        private ChoiceOfCategoryStateData _categoryData;

        private TableProcessorStateData _tableProcessorStateData;
        public TableProcessorState
        (
            StateMachine gameStateMachine, 
            ITableProcessorService tableProcessorService,
            IItemAddationService itemAddationService,
            GlobalUIElements globalUIElements)
        {
            _stateMachine = gameStateMachine;          
            _tableProcessorService = tableProcessorService;
            _addationService = itemAddationService;
            _globalUIElements = globalUIElements;
        }

        public void Enter(TableProcessorStateData tableProcessorStateData)
        {
            _tableProcessorStateData = tableProcessorStateData;

            _categoryData = _tableProcessorStateData.choiceOfCategoryStateData;
            _tableProcessorService.SetTableData(_tableProcessorStateData.selectedListOfTableItems);

            AddUIListeners();
        }

        public void Exit()
        {
            _tableProcessorService.CloseTable();

            RemoveUIListeners();
        }
        private void AddUIListeners()
        {
            _globalUIElements.backButton.onClick.AddListener(Back);
            _globalUIElements.addationButton.onClick.AddListener(Addation);
        }

        private void RemoveUIListeners()
        {
            _globalUIElements.backButton.onClick.RemoveListener(Back);
            _globalUIElements.addationButton.onClick.RemoveListener(Addation);
        }

        private void Addation()
        {
            var addationData = new AddationData(_categoryData.MenuType, _tableProcessorStateData.categoryName, false);
            _addationService.Open(addationData, OnAdded);
        }
        private void OnAdded()
        {
            Enter(_tableProcessorStateData);
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
        public string categoryName;
    }
}