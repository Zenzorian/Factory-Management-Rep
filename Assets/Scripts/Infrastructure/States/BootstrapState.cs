using Scripts.Infrastructure.AssetManagement;
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

            InitializeAndRegisterServicesWithExternalElements();

            _services.RegisterSingle<ITutorialService>(new TutorialService(_services.Single<ISaveloadDataService>(), _services.Single<IAssetProvider>().GetMainMenuButtons()));
            Debug.Log("TutorialService Initialized");

            _services.RegisterSingle<IPopUpMassageService>(new PopupMessageService(_services.Single<IAssetProvider>().GetPopupMessageElements()));
            Debug.Log("PopUpMassageService Initialized");

            _services.RegisterSingle<IConfirmPanelService>(new ConfirmPanelService(_services.Single<IAssetProvider>().GetConfirmationPanelElements()));
            Debug.Log("ConfirmationPanelService Initialized");
        }
        private void InitializeAndRegisterServicesWithExternalElements()
        {
            var assetProvider = Transform.FindFirstObjectByType<AssetProvider>();
            if (assetProvider == null)
            {
                Debug.LogError("AssetProvider not found");
                return;
            }
            else
            {
                Debug.Log("AssetProvider Initialized");
                _services.RegisterSingle<IAssetProvider>(assetProvider);
            }

            var serviceInitializer = Transform.FindFirstObjectByType<ServiceInitializer>();
            if (serviceInitializer == null)
            {
                Debug.LogError("Service Initializer not found");
                return;
            }
            serviceInitializer.Initialize(_services);
        }     
    }
}