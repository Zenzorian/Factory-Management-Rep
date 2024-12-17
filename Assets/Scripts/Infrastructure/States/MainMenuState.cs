
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveloadDataService _saveloadDataService;


        private Button[] _menuButtons;

        public MainMenuState(StateMachine stateMachine, LoadingCurtain loadingCurtain, IAssetProvider assetProvider, ISaveloadDataService saveloadDataService)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;
            _saveloadDataService = saveloadDataService;
        }

        public void Enter()
        {
            Debug.Log("Enter on MainMenuState");

            _saveloadDataService.LoadData();

            _loadingCurtain.Hide();

            SetMenuButtonsEvents();
        }

        private void SetMenuButtonsEvents()
        {
            _menuButtons = _assetProvider.GetMainMenuButtons();

            for (int i = 0; i < _menuButtons.Length; i++)
            {
                Button button = _menuButtons[i];

                var menuType = (MainMenuTypes)i;

                if(menuType == MainMenuTypes.Options)
                    button.onClick.AddListener(() => OpenOptions());
                else if (menuType == MainMenuTypes.Statistic)
                    button.onClick.AddListener(() => OpenStatistic());
                else
                    button.onClick.AddListener(() => OpenCategory(menuType));
            }
        }

        private void OpenOptions()
        {
            
        }

        private void OpenStatistic()
        {

        }

        public void OpenCategory(MainMenuTypes value)
        {
            var categoryData = new ChoiceOfCategoryStateData();

            categoryData.selectedListOfCategotyElements = _saveloadDataService.GetTypesOfItemsListByType((MainMenuTypes)value);
            categoryData.MenuType = value;

            GoToChoiceOfCategoryState(categoryData);
        }
        public void GoToChoiceOfCategoryState(ChoiceOfCategoryStateData categoryData)
        {
            if (categoryData.selectedListOfCategotyElements == null)
            {
                Debug.LogError("Category List Is Null");
                return;
            }
            _stateMachine.Enter<ChoiceOfCategoryState, ChoiceOfCategoryStateData>(categoryData);
        }

        public void Exit()
        {
        }

    }
}
public enum MainMenuTypes
{
    Workspaces,
    Tools,
    Workers,
    Parts,
    Statistic,   
    Options,
}