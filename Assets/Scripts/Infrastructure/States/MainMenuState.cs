using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Scripts.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IElementsProvider _assetProvider;
        private readonly ISaveloadDataService _saveloadDataService;


        private Button[] _menuButtons;

        public MainMenuState(StateMachine stateMachine, LoadingCurtain loadingCurtain, IElementsProvider assetProvider, ISaveloadDataService saveloadDataService)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;
            _saveloadDataService = saveloadDataService;
        }

        public void Enter()
        {
            Debug.Log("=> Enter on MainMenuState <=");

            _saveloadDataService.LoadData();

            _loadingCurtain.Hide();

            SetMenuButtonsEvents();

            _assetProvider.GlobalUIElements.addationButton.gameObject.SetActive(false);
            _assetProvider.GlobalUIElements.editButton.gameObject.SetActive(false);
        }
        public void Exit()
        {
            RemoveMenuButtonsEvents();
            _assetProvider.GlobalUIElements.addationButton.gameObject.SetActive(true);
            _assetProvider.GlobalUIElements.editButton.gameObject.SetActive(true);
        }

        private void SetMenuButtonsEvents()
        {
            _menuButtons = _assetProvider.MainMenu.buttons;

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
            _stateMachine.Enter<SelectionOfStatisticsContextState, SelectedStatisticsContext>(null);
        }

        public void OpenCategory(MainMenuTypes value)
        {
            var categoryData = new ChoiceOfCategoryStateData(value, _saveloadDataService.GetTypesOfItemsListByType((MainMenuTypes)value));

            GoToChoiceOfCategoryState(categoryData);
        }
        public void GoToChoiceOfCategoryState(ChoiceOfCategoryStateData categoryData)
        {
            if (categoryData.selectedListOfCategotyElements == null)
            {
                Debug.LogWarning("Category List Is Null");
                categoryData.selectedListOfCategotyElements = new List<string>();
            }
            _stateMachine.Enter<ChoiceOfCategoryState, ChoiceOfCategoryStateData>(categoryData);
        }

        private void RemoveMenuButtonsEvents()
        {
            _menuButtons = _assetProvider.MainMenu.buttons;

            foreach (var button in _menuButtons)
            {
                button.onClick.RemoveAllListeners();
            }
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