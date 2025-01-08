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
        private readonly IStatisticsInputService _statisticsInputService;

        private List<StatisticData> _currentStatisticData = new List<StatisticData>();
        public ChoiceOfStatisticDataState
        (
            StateMachine stateMachine, 
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpMassageService popUpMassageService,
            IItemAddationService addationService,
            IStatisticsInputService statisticsInputService,
            GlobalUIElements globalUIElements)
            : base(stateMachine, choiceOfCategoryService, popUpMassageService, globalUIElements)
        {
            _addationService = addationService;
            _statisticsInputService = statisticsInputService;
        }

        public override void Enter(ChoiceOfStatisticDataStateData stateData)
        {
            Debug.Log("=> Enter on  Choice Of Statistic Data State <=");

            _currentStateData = stateData;           


            _currentStatisticData = _currentStateData.selectedStatisticData.Data;

            var categoryNames = new List<string>();
            foreach (var item in _currentStatisticData)
            {
                categoryNames.Add($"F = {item.F} V = {item.V}");
            }
            _currentStateData.selectedListOfCategotyElements = categoryNames;

            base.Enter(_currentStateData);
            AddUIListeners();
        }

        protected override void OnChoiceMade(MainMenuTypes menuType, int index)
        {
            StatisticData temporaryData = _currentStatisticData[index];
            _statisticsInputService.ShowPanel(temporaryData);
        }

        protected override void OnBack()
        {           
            _stateMachine.Enter<SelectionOfStatisticState, SelectedStatistic>(_currentStateData.selectedStatistic);
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
            var addationData = new AddationData(_currentStateData.menuType, -1,_currentStatisticData);
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