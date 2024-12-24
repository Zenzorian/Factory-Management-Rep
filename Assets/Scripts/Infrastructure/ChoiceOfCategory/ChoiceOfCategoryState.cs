using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfCategoryState : BaseChoiceOfCategoryState<ChoiceOfCategoryStateData>
    {
        private ChoiceOfCategoryStateData _currentStateData;
        private readonly IItemAddationService _addationService;

        public ChoiceOfCategoryState(
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpMassageService popUpMassageService,
            IItemAddationService addationService,
            GlobalUIElements globalUIElements)
            : base(stateMachine, choiceOfCategoryService, popUpMassageService, globalUIElements)
        {
            _addationService = addationService;
        }

        public override void Enter(ChoiceOfCategoryStateData stateData)
        {
            _currentStateData = stateData; // Сохраняем текущие данные состояния
            base.Enter(stateData);
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

        protected override ChoiceOfCategoryStateData GetCategoryData(ChoiceOfCategoryStateData stateData)
        {
            return stateData;
        }

        protected override void AddUIListeners()
        {
            base.AddUIListeners();
            _globalUIElements.addationButton.onClick.AddListener(OnAddation);
        }

        protected override void RemoveUIListeners()
        {
            base.RemoveUIListeners();
            _globalUIElements.addationButton.onClick.RemoveListener(OnAddation);
        }

        private void OnAddation()
        {
            var addationData = new AddationData(_currentStateData.menuType, -1, false);
            _addationService.Open(addationData, () => Enter(_currentStateData));
        }
    }


    public class ChoiceOfCategoryStateData
    {
        public ChoiceOfCategoryStateData(MainMenuTypes menuType, List<string> selectedListOfCategotyElements)
        {
            this.menuType = menuType;
            this.selectedListOfCategotyElements = selectedListOfCategotyElements;
        }
        public MainMenuTypes menuType;
        public List<string> selectedListOfCategotyElements;
    }
}