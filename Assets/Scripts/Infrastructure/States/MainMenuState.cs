
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
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

                int menuType = i;
                button.onClick.AddListener(() => OpenMenu(menuType));
            }
        }

        public void OpenMenu(int value)
        {
            var categoryData = new CategoryData();

            switch ((MainMenuTypes)value)
            {
                case MainMenuTypes.Workspaces:
                    categoryData.selectedList = _saveloadDataService.GetTypesOfWorkspace();
                    categoryData.MenuType = MainMenuTypes.Workspaces;
                    GoToChoiceOfCategoryState(categoryData);
                    return;
                case MainMenuTypes.Tools:
                    categoryData.selectedList = _saveloadDataService.GetTypesOfTools();
                    categoryData.MenuType = MainMenuTypes.Tools;
                    GoToChoiceOfCategoryState(categoryData);
                    return;
                case MainMenuTypes.Workers:
                    categoryData.selectedList = _saveloadDataService.GetTypesOfWorkers();
                    GoToChoiceOfCategoryState(categoryData);
                    return;
                case MainMenuTypes.Parts:
                    categoryData.selectedList = _saveloadDataService.GetTypesOfParts();
                    GoToChoiceOfCategoryState(categoryData);
                    return;
                case MainMenuTypes.Statistic:
                    _stateMachine.Enter<StatisticProcessorState>();
                    break;
                //case MainMenuTypes.StatisticPart:
                //    selectedList = _globalData.typesOfParts;
                //    break;
                //case MainMenuTypes.StatisticTool:
                //    selectedList = _globalData.typesOfTools;
                //    break;
                //case MainMenuTypes.Options:
                //    Forward(_optionsPanel.gameObject);
                //    return;
                default:
                    break;
            }            
        }
        public void GoToChoiceOfCategoryState(CategoryData categoryData)
        {
            if (categoryData.selectedList == null)
            {
                Debug.LogError("Category List Is Null");
                return;
            }
            _stateMachine.Enter<ChoiceOfCategoryState, CategoryData>(categoryData);
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
    StatisticPart,
    StatisticTool,
    Options,
}