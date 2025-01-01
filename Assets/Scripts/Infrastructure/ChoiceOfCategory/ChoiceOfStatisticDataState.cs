using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfStatisticDataState : BaseChoiceOfCategoryState<ChoiceOfStatisticDataStateData>
    {
        private ChoiceOfStatisticDataStateData _currentStateData;
        private readonly IItemAddationService _addationService;
               
        private List<StatisticData> _currentStatisticData = new List<StatisticData>();
        public ChoiceOfStatisticDataState
        (
            StateMachine stateMachine, 
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpMassageService popUpMassageService,
            IItemAddationService addationService,
            GlobalUIElements globalUIElements)
            : base(stateMachine, choiceOfCategoryService, popUpMassageService, globalUIElements)
        {
            _addationService = addationService;
        }

        public override void Enter(ChoiceOfStatisticDataStateData stateData)
        {
            Debug.Log("=> Enter on Statistic Edit Choice Of Category State <=");

            _currentStateData = stateData;           


            _currentStatisticData = _currentStateData.selectedStatisticData.Data;

            var categoryNames = new List<string>();
            foreach (var item in _currentStatisticData)
            {
                categoryNames.Add($"F = {item.F} V = { item.V}");
            }
            _currentStateData.selectedListOfCategotyElements = categoryNames;

            base.Enter(stateData);
            AddUIListeners();
        }

        protected override void OnChoiceMade(MainMenuTypes menuType, int index)
        {
            var tableProcessorStateData = new TableProcessorStateData
            {
                choiceOfCategoryStateData = _currentStateData,
                indexOfSelectedCategoty = index
            };
            _stateMachine.Enter<TableProcessorState, TableProcessorStateData>(tableProcessorStateData);
        }

        protected override void OnBack()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        protected override ChoiceOfCategoryStateData GetCategoryData(ChoiceOfStatisticDataStateData stateData)
        {
            return stateData;
        }

        protected override void AddUIListeners()
        {
            base.AddUIListeners();
            _globalUIElements.addationButton.onClick.AddListener(OnAddation);
        }
          
        private void OnAddation()
        {
            var addationData = new AddationData(_currentStateData.menuType, -1);
            _addationService.Open(addationData, () => Enter(_currentStateData));
        }
    }
    public class ChoiceOfStatisticDataStateData : StatisticChoiceOfCategoryStateData
    {
        public Statistic selectedStatisticData;

        public ChoiceOfStatisticDataStateData
        (
            MainMenuTypes menuType,
            List<string> selectedListOfCategotyElements,
            SelectedStatistic selectedStatistic,
            Statistic selectedStatisticData
        ) : base(menuType, selectedListOfCategotyElements, selectedStatistic)
        {
            this.selectedStatisticData = selectedStatisticData;
        }
    }

    
}