using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfCategoryState : IPayloadedState<ChoiceOfCategoryStateData>
    {
        private readonly StateMachine _stateMachine;
        private readonly IChoiceOfCategoryService _choiceOfCategoryService;
        private readonly IPopUpMassageService _popUpMassageService;
        private readonly IItemAddationService _addationService;

        private readonly GlobalUIElements _globalUIElements;


        private UnityEvent<MainMenuTypes, int> _choiceButtonPressed = new UnityEvent<MainMenuTypes, int>();

        private ChoiceOfCategoryStateData _categoryData;
        public ChoiceOfCategoryState(StateMachine stateMachine, IChoiceOfCategoryService choiceOfCategoryService, IPopUpMassageService popUpMassageService, IItemAddationService addationService, GlobalUIElements globalUIElements)
        {
            _stateMachine = stateMachine;
            _choiceOfCategoryService = choiceOfCategoryService;
            _globalUIElements = globalUIElements;
            _addationService = addationService;
            _popUpMassageService = popUpMassageService;

        }
        public void Enter(ChoiceOfCategoryStateData categoryData)
        {
            _categoryData = categoryData;
            _choiceOfCategoryService.Create(categoryData.selectedListOfCategotyElements, categoryData.MenuType, _choiceButtonPressed);
            _choiceOfCategoryService.Activate();

            _choiceButtonPressed.AddListener(listOfItemsChosen);

            AddUIListeners();
        }
        private void AddUIListeners()
        {
            _globalUIElements.backButton.onClick.AddListener(Back);
            _globalUIElements.addationButton.onClick.AddListener(Addation);
        }
        private void Addation()
        {
            var addationData = new AddationData(_categoryData.MenuType,-1, false);
            _addationService.Open(addationData, OnAdded);
        }
        private void OnAdded()
        {
            Enter(_categoryData);
        }
        private void Back()
        {
            _stateMachine.Enter<MainMenuState>();
        }
        private void listOfItemsChosen(MainMenuTypes menuType, int indexOfSelectedCategoty)
        {
            var tableProcessorStateData = new TableProcessorStateData();
            tableProcessorStateData.choiceOfCategoryStateData = _categoryData;
            tableProcessorStateData.indexOfSelectedCategoty = indexOfSelectedCategoty;

            _stateMachine.Enter<TableProcessorState, TableProcessorStateData>(tableProcessorStateData);

        }
        public void Exit()
        {
            _choiceOfCategoryService.Deactivate();
            RemoveUIListeners();
        }

        private void RemoveUIListeners()
        {
            _globalUIElements.backButton.onClick.RemoveListener(Back);
            _globalUIElements.addationButton.onClick.RemoveListener(Addation);
        }
    }
    public struct ChoiceOfCategoryStateData
    {
        public MainMenuTypes MenuType;
        public List<string> selectedListOfCategotyElements;
    }
}