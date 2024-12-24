using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class StatisticChoiceOfCategoryState : BaseChoiceOfCategoryState<StatisticChoiceOfCategoryStateData>
    {
        private StatisticChoiceOfCategoryStateData _currentStateData;

        public StatisticChoiceOfCategoryState(
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpMassageService popUpMassageService,
            GlobalUIElements globalUIElements)
            : base(stateMachine, choiceOfCategoryService, popUpMassageService, globalUIElements) { }

        public override void Enter(StatisticChoiceOfCategoryStateData stateData)
        {
            _currentStateData = stateData;
            base.Enter(stateData);
        }

        protected override void OnChoiceMade(MainMenuTypes menuType, int index)
        {
            var stateData = new StatisticTableProcessorStateData(_currentStateData, index);
            _stateMachine.Enter<StatisticTableProcessorState, StatisticTableProcessorStateData>(stateData);
        }

        protected override void OnBack()
        {
            _stateMachine.Enter<SelectionOfStatisticState, SelectedStatisticData>(_currentStateData.selectedStatisticData);
        }

        protected override ChoiceOfCategoryStateData GetCategoryData(StatisticChoiceOfCategoryStateData stateData)
        {
            return stateData;
        }
    }


    public class StatisticChoiceOfCategoryStateData : ChoiceOfCategoryStateData
    {
        public SelectedStatisticData selectedStatisticData;

        public StatisticChoiceOfCategoryStateData
        (
            MainMenuTypes menuType,
            List<string> selectedListOfCategotyElements,
            SelectedStatisticData selectedStatisticData
        ) : base(menuType, selectedListOfCategotyElements)
        {
            this.selectedStatisticData = selectedStatisticData;
        }
    }
}