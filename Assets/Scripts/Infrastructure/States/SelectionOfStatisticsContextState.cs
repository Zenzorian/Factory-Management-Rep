using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services.Statistics;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class SelectionOfStatisticsContextState : IPayloadedState<SelectedStatisticsContext>
    {
        private readonly StateMachine _stateMachine;
        private readonly IChoiceOfStatisticService _statisticService;       
        private readonly GlobalUIElements _globalUIElements;

        public SelectionOfStatisticsContextState
        (
              StateMachine gameStateMachine,
              IChoiceOfStatisticService statisticService,              
              GlobalUIElements globalUIElements
        )
        {
            _stateMachine = gameStateMachine;
            _statisticService = statisticService;           
            _globalUIElements = globalUIElements;
        }

        public void Enter(SelectedStatisticsContext selectedStatisticData = null)
        {
            Debug.Log("=> Enter on Selection Of Statistic State <=");

            _statisticService.ShowPanel(_stateMachine, selectedStatisticData);

            Initialize();
        }
        public void Exit()
        {
            PutOff();
        }

        private void Initialize()
        {
            _globalUIElements.backButton.onClick.AddListener(Back);
            _globalUIElements.addationButton.gameObject.SetActive(false);
        }


        private void PutOff()
        {
            _globalUIElements.backButton.onClick.RemoveListener(Back);
            _globalUIElements.addationButton.gameObject.SetActive(true);
            _statisticService.HidePanel();
        }

        private void Back()
        {
            _stateMachine.Enter<MainMenuState>();
        }
    }
    public class SelectedStatisticsContext
    {
        public Part selectedPart;
        //public ProcessingType selectedProcessingType;
        //public Tool selectedTool;       
    }
}