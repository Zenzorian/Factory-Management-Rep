using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class StatisticGrafViewState : IPayloadedState<StatisticGrafViewStateData>
    {
        private readonly StateMachine _stateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IStatisticsGraphViewService _statisticsGraphViewService;
        private readonly IElementsProvider _assetProvider;

        private StatisticGrafViewStateData _stateData;

        public StatisticGrafViewState
        (
            StateMachine stateMachine,
            LoadingCurtain loadingCurtain,
            IStatisticsGraphViewService statisticsGraphViewService,
            IElementsProvider assetProvider
        )
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _statisticsGraphViewService = statisticsGraphViewService;
            _assetProvider = assetProvider;           
        }
        
        public void Enter(StatisticGrafViewStateData data)
        {
            Debug.Log("=> Enter on Statistic Graf View State <=");

            _stateData = data;
                                               
            Initialize();
            _statisticsGraphViewService.Initialize(_stateData.statistic);

            _loadingCurtain.Hide();
        }

        public void Exit()
        {
            _statisticsGraphViewService.Clear();
            PutOff();
        }

        private void Back()
        {
            _stateMachine.Enter<SelectionOfStatisticsContextState, SelectedStatisticsContext>(_stateData.selectedStatisticsContext);
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
    public class StatisticGrafViewStateData
    {
        public Statistic statistic;
        public SelectedStatisticsContext selectedStatisticsContext;

        public StatisticGrafViewStateData(Statistic statistic, SelectedStatisticsContext selectedStatisticsContext)
        {
            this.statistic = statistic;
            this.selectedStatisticsContext = selectedStatisticsContext;            
        }
    }
}