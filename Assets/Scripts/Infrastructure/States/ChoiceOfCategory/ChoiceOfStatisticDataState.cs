using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using Scripts.UI.Markers;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfStatisticDataState : BaseChoiceOfCategoryState<ChoiceOfStatisticDataStateData>
    {
        private ChoiceOfStatisticDataStateData _currentStateData;

        private readonly IItemAddationService _addationService;
        private readonly IStatisticsInputService _statisticsInputService;
        private readonly ISaveloadDataService _saveloadDataService;       
        
        public ChoiceOfStatisticDataState
        (
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            ISaveloadDataService saveloadDataService,
            IPopUpService popUpService,   
            IItemAddationService addationService,
            IStatisticsInputService statisticsInputService,
            GlobalUIElements globalUIElements)
            : base(stateMachine, choiceOfCategoryService, popUpService, globalUIElements)
        {
            _addationService = addationService;
            _statisticsInputService = statisticsInputService;
            _saveloadDataService = saveloadDataService;           
        }

        public override void Enter(ChoiceOfStatisticDataStateData stateData)
        {
            Debug.Log("=> Enter on  Choice Of Statistic Data State <=");

            _currentStateData = stateData;           
          
            SetData();           

            base.Enter(_currentStateData);
            _choiceOfCategoryService.Create(_currentStateData.selectedListOfCategotyElements, _currentStateData.menuType, _choiceButtonPressed, OnDelete);
            AddUIListeners();
        }

        private void SetData()
        {
            var categoryNames = new List<string>();
            foreach (var item in  _currentStateData.selectedStatisticData.Data)
            {
                categoryNames.Add($"F = {item.F} V = {item.V}");
            }
            _currentStateData.selectedListOfCategotyElements = categoryNames;
        }

        protected override void OnChoiceMade(MainMenuTypes menuType, int index)
        {
            StatisticData temporaryData =  _currentStateData.selectedStatisticData.Data[index];
            _statisticsInputService.ShowPanel(temporaryData);
        }

        protected override void OnBack()
        {           
            _stateMachine.Enter<SelectionOfStatisticsContextState, SelectedStatisticsContext>(_currentStateData.selectedStatistic);
        }

        protected override ChoiceOfCategoryStateData GetCategoryData(ChoiceOfStatisticDataStateData stateData)
        {
            return stateData;
        }

        protected override void AddUIListeners()
        {
            base.AddUIListeners();
            _globalUIElements.addationButton.onClick.AddListener(OnAddation);
            _globalUIElements.editButton.onClick.AddListener(OnEdit);
            _globalUIElements.showGraphButton.gameObject.SetActive(true);
            _globalUIElements.showGraphButton.onClick.AddListener(OnShowGraph);
        }
          
        private void OnAddation()
        {
            var addationData = new AddationData(_currentStateData.menuType, -1,_currentStateData.selectedStatisticData.Data);
            _addationService.Open(addationData, () => Enter(_currentStateData));
        }   

        private void OnEdit()
        {
            _choiceOfCategoryService.Edit();
        }
        private void OnDelete(int index)
        {           
            _popUpService.ShowConfirm("Are you sure you want to delete this category?",
                ()=>DeleteCategory(index));
        }
        private void DeleteCategory(int index)
        {
            _currentStateData.selectedStatisticData.Data.RemoveAt(index);
           
            SetData();   

            _choiceOfCategoryService.Create
            (
                _currentStateData.selectedListOfCategotyElements,
                _currentStateData.menuType,
                _choiceButtonPressed, 
                OnDelete
            );
        }
        private void OnShowGraph()
        {
            var statisticGrafViewStateData = new StatisticGrafViewStateData(_currentStateData.selectedStatisticData, _currentStateData.selectedStatistic);  
            _stateMachine.Enter<StatisticGrafViewState, StatisticGrafViewStateData>(statisticGrafViewStateData);
        }
    }
    public class ChoiceOfStatisticDataStateData : StatisticChoiceOfCategoryStateData
    {
        public Statistic selectedStatisticData;

        public ChoiceOfStatisticDataStateData
        (
            MainMenuTypes menuType,
            List<string> selectedListOfCategotyElements,
            SelectedStatisticsContext selectedStatistic,
            Statistic selectedStatisticData
        ) : base(menuType, selectedListOfCategotyElements, selectedStatistic)
        {
            this.selectedStatisticData = selectedStatisticData;
        }
    }

    
}