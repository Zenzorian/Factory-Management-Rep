﻿using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using Scripts.UI.Markers;
namespace Scripts.Infrastructure.States
{
    public class StatisticSelectionChoiceOfCategoryState : BaseChoiceOfCategoryState<StatisticChoiceOfCategoryStateData>
    {
        private StatisticChoiceOfCategoryStateData _currentStateData;

        public StatisticSelectionChoiceOfCategoryState
        (
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpService popUpService,
            GlobalUIElements globalUIElements
        ) : base(stateMachine, choiceOfCategoryService, popUpService, globalUIElements) { }

        public override void Enter(StatisticChoiceOfCategoryStateData stateData)
        {
            Debug.Log("=> Enter on Statistic Choice Of Category State Data <=");

            _currentStateData = stateData;

            base.Enter(stateData);
            AddUIListeners();
        }
       
        protected override void OnChoiceMade(MainMenuTypes menuType, int index)
        {
            var stateData = new StatisticTableProcessorStateData(_currentStateData, index);
            _stateMachine.Enter<StatisticTableProcessorState, StatisticTableProcessorStateData>(stateData);
        }

        protected override void OnBack()
        {
            _stateMachine.Enter<SelectionOfStatisticsContextState, SelectedStatisticsContext>(_currentStateData.selectedStatistic);
        }

        protected override ChoiceOfCategoryStateData GetCategoryData(StatisticChoiceOfCategoryStateData stateData)
        {
            return stateData;
        }
        protected override void AddUIListeners()
        {
            base.AddUIListeners();
            _globalUIElements.addationButton.gameObject.SetActive(false);
            _globalUIElements.editButton.gameObject.SetActive(false);
        }
        protected override void RemoveUIListeners()
        {
            _globalUIElements.addationButton.gameObject.SetActive(true);
            _globalUIElements.editButton.gameObject.SetActive(true);
            base.RemoveUIListeners();
        }
    }


    public class StatisticChoiceOfCategoryStateData : ChoiceOfCategoryStateData
    {
        public SelectedStatisticsContext selectedStatistic;

        public StatisticChoiceOfCategoryStateData
        (
            MainMenuTypes menuType,
            List<string> selectedListOfCategotyElements,
            SelectedStatisticsContext selectedStatistic
        ) : base(menuType, selectedListOfCategotyElements)
        {
            this.selectedStatistic = selectedStatistic;
        }
    }
}