using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class StatisticTableProcessorState : IPayloadedState<StatisticTableProcessorStateData>
    {
        private readonly StateMachine _stateMachine;
        
        private readonly ITableProcessorService _tableProcessorService;       
        private readonly GlobalUIElements _globalUIElements;        

        private StatisticTableProcessorStateData _stateData;
        public StatisticTableProcessorState
        (
            StateMachine gameStateMachine, 
            ITableProcessorService tableProcessorService,           
            GlobalUIElements globalUIElements)
        {
            _stateMachine = gameStateMachine;          
            _tableProcessorService = tableProcessorService;           
            _globalUIElements = globalUIElements;
        }

        public void Enter(StatisticTableProcessorStateData stateData)
        {   
            _stateData = stateData;

            _tableProcessorService.SetTableData
                (_stateData.choiceData.menuType, _stateData.indexOfSelectedCategoty, CellSelected);

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
            _globalUIElements.addationButton.gameObject.SetActive(false);
        }

        private void RemoveUIListeners()
        {
            _globalUIElements.backButton.onClick.RemoveListener(Back);
            _globalUIElements.addationButton.gameObject.SetActive(true);
        }
          
        private void Back()
        {
            _stateMachine.Enter<StatisticChoiceOfCategoryState, StatisticChoiceOfCategoryStateData>(_stateData.choiceData);           
        }
        private void CellSelected(TableItem tableItem)
        {
            SelectedStatisticData selectedStatisticData = _stateData.choiceData.selectedStatisticData;

            if (_stateData.choiceData.menuType == MainMenuTypes.Parts)
                selectedStatisticData.selectedPart = tableItem as Part;          
            else if (_stateData.choiceData.menuType == MainMenuTypes.Tools)
                selectedStatisticData.selectedTool= tableItem as Tool;           

            _stateMachine.Enter<SelectionOfStatisticState, SelectedStatisticData>(selectedStatisticData);
        }
    }
    
    public class StatisticTableProcessorStateData
    {
        public StatisticChoiceOfCategoryStateData choiceData;
        public int indexOfSelectedCategoty;
        public StatisticTableProcessorStateData(StatisticChoiceOfCategoryStateData choiceData, int indexOfSelectedCategoty)
        {
            this.choiceData = choiceData;
            this.indexOfSelectedCategoty = indexOfSelectedCategoty;
        }
    }
}