using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using Scripts.Services.Statistics;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class SelectionOfStatisticState : IPayloadedState<SelectedStatisticData>
    {
        private readonly StateMachine _stateMachine;
        private readonly IChoiceOfStatisticDataService _statisticService;
        private readonly IItemAddationService _itemAddationService;
        private readonly GlobalUIElements _globalUIElements;

        public SelectionOfStatisticState
        (
              StateMachine gameStateMachine,
              IChoiceOfStatisticDataService statisticService,
              IItemAddationService itemAddationService,
              GlobalUIElements globalUIElements
        )
        {
            _stateMachine = gameStateMachine;
            _statisticService = statisticService;
            _itemAddationService = itemAddationService;
            _globalUIElements = globalUIElements;
        }

        public void Enter(SelectedStatisticData selectedStatisticData = null)
        {
            _statisticService.ShowPanel(_stateMachine, selectedStatisticData);

            _globalUIElements.backButton.onClick.AddListener(Back);
            _globalUIElements.addationButton.gameObject.SetActive(false);
        }

        public void Exit()
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
    public class SelectedStatisticData
    {
        public Part selectedPart;
        public Tool selectedTool;
        public ProcessingType selectedProcessingType;
    }
}