using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using System;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfCategoryState : IPayloadedState<CategoryData>
    {
        private readonly StateMachine _stateMachine;
        private readonly IChoiceOfCategoryService _choiceOfCategoryService;
        private readonly GlobalUIElements _globalUIElements;
        public ChoiceOfCategoryState(StateMachine stateMachine, IChoiceOfCategoryService choiceOfCategoryService, GlobalUIElements globalUIElements)
        {
            _stateMachine = stateMachine;
            _choiceOfCategoryService = choiceOfCategoryService;
            _globalUIElements = globalUIElements;
        }
        public void Enter(CategoryData categoryData)
        {
            _choiceOfCategoryService.Create(categoryData.selectedList, categoryData.MenuType);
            _choiceOfCategoryService.Activate();

            _globalUIElements.backButton.onClick.AddListener(Back);
            //_choiceOfCategoryService.AddationButtonPressed.AddListener(Addation);
        }
        private void Addation()
        {
            throw new NotImplementedException();
        }

        private void Back()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
            _choiceOfCategoryService.Deactivate();
            _globalUIElements.backButton.onClick.RemoveListener(Back);
            //_choiceOfCategoryService.AddationButtonPressed.RemoveListener(Addation);
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
}
public struct CategoryData
{
    public MainMenuTypes MenuType;
    public List<string> selectedList;
}