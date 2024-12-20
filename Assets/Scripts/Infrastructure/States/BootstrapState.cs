﻿using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
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

            var assetProvider = _services.Single<IUIElementsProvider>();

            _services.RegisterSingle<ISaveloadDataService>(new SaveloadDataService());
            Debug.Log("SaveloadDataService Initialized");

            _services.RegisterSingle<ITutorialService>(new TutorialService(_services.Single<ISaveloadDataService>(), assetProvider.GetMainMenuButtons()));
            Debug.Log("TutorialService Initialized");

            _services.RegisterSingle<IPopUpMassageService>(new PopupMessageService(assetProvider.GetPopupMessageElements()));
            Debug.Log("PopUpMassageService Initialized");

            _services.RegisterSingle<IConfirmPanelService>(new ConfirmPanelService(assetProvider.GetConfirmationPanelElements()));
            Debug.Log("ConfirmationPanelService Initialized");

            _services.RegisterSingle<IChoiceOfCategoryService>(new ChoiceOfCategoryService(assetProvider.GetChoiceOfCategoryElements(), _services.Single<ISaveloadDataService>()));
            Debug.Log("ConfirmationPanelService Initialized");

            _services.RegisterSingle<ITableProcessorService>(new TableProcessor(_services.Single<ISaveloadDataService>(), GetTableView()));
            Debug.Log("TableProcessorService Initialized");
          
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
            var assetProvider = Transform.FindFirstObjectByType<UIElementsProvider>();
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