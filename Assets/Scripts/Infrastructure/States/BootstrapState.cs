using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using Scripts.Services.Statistics;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(StateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            Debug.Log("=> Enter on Bootstrap State  <=");

            _stateMachine.Enter<MainMenuState>();
        }
        public void Exit()
        {
            Debug.Log("BootstrapState Finish");
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IStateMachine>(_stateMachine);

            RegisterAssetProvider();

            var uiElementsProvider = _services.Single<IUIElementsProvider>();

            _services.RegisterSingle<ISaveloadDataService>(new SaveloadDataService());
            Debug.Log("SaveloadDataService Initialized");

            _services.RegisterSingle<IButtonCreator>
            (
                new ButtonCreator(uiElementsProvider.ChoiceOfCategoryElements.choiceButtonPrefab)
            );
            Debug.Log("SaveloadDataService Initialized");

            _services.RegisterSingle<ITutorialService>
            (
                new TutorialService
                (
                    _services.Single<ISaveloadDataService>(),
                    uiElementsProvider.MainMenu.buttons
                )
            );
            Debug.Log("TutorialService Initialized");

            _services.RegisterSingle<IPopUpMassageService>
                (new PopupMessageService(uiElementsProvider.PopupMessageElements));
            Debug.Log("PopUpMassageService Initialized");

            _services.RegisterSingle<IConfirmPanelService>
                (new ConfirmPanelService(uiElementsProvider.ConfirmationPanelElements));
            Debug.Log("ConfirmationPanelService Initialized");

            _services.RegisterSingle<IChoiceOfCategoryService>
            (
                new ChoiceOfCategoryService
                (
                    _services.Single<ISaveloadDataService>(),
                    _services.Single<IButtonCreator>(),
                    uiElementsProvider.ChoiceOfCategoryElements
                )
            );
            Debug.Log("ConfirmationPanelService Initialized");

            _services.RegisterSingle<ITableProcessorService>
            (
                new TableProcessor(_services.Single<ISaveloadDataService>(),
                GetTableView())
            );
            Debug.Log("TableProcessorService Initialized");

            _services.RegisterSingle<IStatisticsInputService>
            (
                new StatisticsInputService
                (
                    _services.Single<ISaveloadDataService>(),
                    _services.Single<IButtonCreator>(),
                    uiElementsProvider.StatisticsInputElements,
                    uiElementsProvider.GlobalUIElements
                )
            );
            Debug.Log("StatisticsInputService Initialized");

            _services.RegisterSingle<IChoiceOfStatisticService>
            (
                new ChoiceOfStatisticService
                (
                    _services.Single<ISaveloadDataService>(),
                    _services.Single<IPopUpMassageService>(),
                    _services.Single<IConfirmPanelService>(),
                    uiElementsProvider.StatisticViewElements
                )
            );
            Debug.Log("Choice Of StatisticService Initialized");

            _services.RegisterSingle<IStatisticsGraphViewService>
           (
               new StatisticsGraphViewService()
           );
        }

        private TableView GetTableView()
        {
            var tableView = Transform.FindFirstObjectByType<TableView>();
            if (tableView == null)
            {
                Debug.LogError("tableView not found");
                return null;
            }
            else return tableView;
        }

        private void RegisterAssetProvider()
        {
            var assetProvider = Transform.FindFirstObjectByType<ElementsProvider>();
            if (assetProvider == null)
            {
                Debug.LogError("AssetProvider not found");
                return;
            }
            else
            {
                Debug.Log("AssetProvider Initialized");
                _services.RegisterSingle<IUIElementsProvider>(assetProvider);
            }
        }
    }
}