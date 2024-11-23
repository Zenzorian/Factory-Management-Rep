
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly LoadingCurtain _loadingCurtain;

        private Button[] _menuButtons;

        public MainMenuState(StateMachine stateMachine, LoadingCurtain loadingCurtain, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            Debug.Log("Enter on MainMenuState");

            _loadingCurtain.Hide();
           
            _menuButtons = _assetProvider.GetMainMenuButtons();
            
            int menuType;

            for (int i = 0; i < _menuButtons.Length; i++)
            {
                menuType = i;
                _menuButtons[i].onClick.AddListener(() => OpenMenu(menuType));
            }
        }
        public void OpenMenu(int value)
        {
            List<string> selectedList = null;

            Debug.Log(value);

            switch ((MainMenuTypes)value)
            {
                case MainMenuTypes.Workspaces:
                    selectedList = _saveloadDataService.GetTypesOfWorkspace();
                    break;
                case MainMenuTypes.Tools:
                    selectedList = _saveloadDataService.GetTypesOfTools();
                    break;
                case MainMenuTypes.Workers:
                    selectedList = _saveloadDataService.GetTypesOfWorkers();
                    break;
                case MainMenuTypes.Parts:
                    selectedList = _saveloadDataService.GetTypesOfParts();
                    break;
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

            //if (selectedList != null)
            //{
            //    TemporaryListOfCategory = selectedList;
            //    _categoryMenu.Create(selectedList, menuType);
            //    Forward(_choicePanel.gameObject);
            //}
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