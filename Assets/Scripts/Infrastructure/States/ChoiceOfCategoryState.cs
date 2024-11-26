using Scripts.Data;
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
        private readonly GlobalUIElements _globalUIElements;      


        private UnityEvent<List<TableItem>> _choiceButtonPressed = new UnityEvent<List<TableItem>>();
           
        private ChoiceOfCategoryStateData _categoryData;
        public ChoiceOfCategoryState(StateMachine stateMachine, IChoiceOfCategoryService choiceOfCategoryService, IPopUpMassageService popUpMassageService, GlobalUIElements globalUIElements)
        {
            _stateMachine = stateMachine;
            _choiceOfCategoryService = choiceOfCategoryService;
            _globalUIElements = globalUIElements;
            _popUpMassageService = popUpMassageService;
        }
        public void Enter(ChoiceOfCategoryStateData categoryData)
        {
            _categoryData = categoryData;
            _choiceOfCategoryService.Create(categoryData.selectedListOfCategotyElements, categoryData.MenuType, _choiceButtonPressed);
            _choiceOfCategoryService.Activate();

            _choiceButtonPressed.AddListener(listOfItemsChosen);

            _globalUIElements.backButton.onClick.AddListener(Back);
            _globalUIElements.addationButton.onClick.AddListener(Addation);           
        }
        private void Addation()
        {

        }

        private void Back()
        {
            _stateMachine.Enter<MainMenuState>();
        }
        private void listOfItemsChosen(List<TableItem> tableItems)
        {      
            if (tableItems == null || tableItems.Count == 0)
            {
                _popUpMassageService.Show("Current table is empty");
                return;
            }        
            var tableProcessorStateData = new TableProcessorStateData();
            tableProcessorStateData.choiceOfCategoryStateData = _categoryData;
            tableProcessorStateData.selectedListOfTableItems = tableItems;

            _stateMachine.Enter<TableProcessorState,TableProcessorStateData>(tableProcessorStateData);

        }
        public void Exit()
        {
            _choiceOfCategoryService.Deactivate();
            _globalUIElements.backButton.onClick.RemoveListener(Back);
            _globalUIElements.addationButton.onClick.RemoveListener(Addation);
        }
        ////private void Awake()
        ////{            
        ////    AddationManager.instance.OnAdded.AddListener(SomethingAdded);
        ////}       
        //private void SomethingAdded()
        //{
        //    if (MenuManager.Instance.menuType == MainMenuTypes.StatisticTool)
        //    {
        //        CreateForStatistic(_selectedStatisticList);
        //        return;
        //    }

        //    Create(_selectedCategories, MenuType);
        //}
    }
    public struct ChoiceOfCategoryStateData
    {
        public MainMenuTypes MenuType;
        public List<string> selectedListOfCategotyElements;        
    }
}
