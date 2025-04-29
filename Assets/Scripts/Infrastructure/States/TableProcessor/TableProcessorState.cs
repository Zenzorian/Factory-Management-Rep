using Scripts.Services;
using UnityEngine;  
using Scripts.UI.Markers;

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
            Debug.Log("=> Enter on Table Processor State <=");

            RemoveUIListeners();

            _tableProcessorStateData = tableProcessorStateData;

            _categoryData = _tableProcessorStateData.choiceOfCategoryStateData;
            _tableProcessorService.SetTableData
                (_tableProcessorStateData.choiceOfCategoryStateData.menuType, _tableProcessorStateData.indexOfSelectedCategoty);

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
            var addationData = new AddationData(_categoryData.menuType, _tableProcessorStateData.indexOfSelectedCategoty);
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
    
    public class TableProcessorStateData
    {
        public ChoiceOfCategoryStateData choiceOfCategoryStateData;       
        public int indexOfSelectedCategoty;
    }
}