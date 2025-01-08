﻿using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class StatisticGrafViewState : IPayloadedState<Statistic>
    {
        private readonly StateMachine _stateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IStatisticsGraphViewService _statisticsGraphViewService;
        private readonly IUIElementsProvider _assetProvider;
             
        public StatisticGrafViewState
        (
            StateMachine stateMachine,
            LoadingCurtain loadingCurtain,
            IStatisticsGraphViewService statisticsGraphViewService,
            IUIElementsProvider assetProvider
        )
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;           
        }
        
        public void Enter(Statistic data)
        {
            Debug.Log("=> Enter on Statistic Graf View State <=");

            _loadingCurtain.Hide();
            _statisticsGraphViewService.Initialize(data);
        }

        public void Exit()
        {
            
        }

        private void Back()
        {

        }

        private void Initialize() 
        {
            _assetProvider.MainMenu.gameObject.SetActive(false);
            _assetProvider.GlobalUIElements.addationButton.gameObject.SetActive(false);
            _assetProvider.GlobalUIElements.backButton.onClick.AddListener(Back);

        }
        private void PutOff()
        {
            _assetProvider.MainMenu.gameObject.SetActive(true);
            _assetProvider.GlobalUIElements.addationButton.gameObject.SetActive(true);
            _assetProvider.GlobalUIElements.backButton.onClick.RemoveAllListeners();
        }
       
    }
}