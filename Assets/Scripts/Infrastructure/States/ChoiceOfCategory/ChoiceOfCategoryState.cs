using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfCategoryState : BaseChoiceOfCategoryState<ChoiceOfCategoryStateData>
    {
        private ChoiceOfCategoryStateData _currentStateData;
        private readonly IItemAddationService _addationService;

        public ChoiceOfCategoryState(
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpService popUpService,
            IItemAddationService addationService,
            GlobalUIElements globalUIElements)
            : base(stateMachine, choiceOfCategoryService, popUpService, globalUIElements)
        {
            _addationService = addationService;
        }

        public override void Enter(ChoiceOfCategoryStateData stateData)
        {
            Debug.Log("=> Enter on Choice Of Category State <=");

            _currentStateData = stateData; 
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

        protected override ChoiceOfCategoryStateData GetCategoryData(ChoiceOfCategoryStateData stateData)
        {
            return stateData;
        }

        protected override void AddUIListeners()
        {
            base.AddUIListeners();
            _globalUIElements.addationButton.onClick.AddListener(OnAddation);
            _globalUIElements.editButton.onClick.AddListener(OnEdit);
        }
    
        private void OnAddation()
        {
            Debug.Log("OnAddation");
            var addationData = new AddationData(_currentStateData.menuType, -1);
            _addationService.Open(addationData, () => Enter(_currentStateData));
        }

        private void OnEdit()
        {           
            _choiceOfCategoryService.Edit();
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