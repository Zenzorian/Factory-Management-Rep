using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.UI;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticDataService : IChoiceOfStatisticDataService
    {
        private SelectedStatisticData _selectedStatisticData;

        private IStateMachine _stateMachine;
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly ChoiceOfStatisticDataView _view;
        public ChoiceOfStatisticDataService
         (
            ISaveloadDataService saveloadDataService,
            StatisticViewElements statisticViewElements
        )
        {
            _saveloadDataService = saveloadDataService;
            _view = new ChoiceOfStatisticDataView(statisticViewElements);
        }
        private void RegisterEvents()
        {
            _view.SelectPartButton.onClick.AddListener(OnPartButtonClicked);
            _view.SelectToolButton.onClick.AddListener(OnToolButtonClicked);
            _view.GoToStatisticsButton.onClick.AddListener(OnGoToStatisticsButtonClicked);
            _view.ProcessingTypeDropdown.onValueChanged.AddListener(OnProcessingTypeSelected);
        }
        private void AnregisterEvents()
        {
            _view.SelectPartButton.onClick.RemoveListener(OnPartButtonClicked);
            _view.SelectToolButton.onClick.RemoveListener(OnToolButtonClicked);
            _view.GoToStatisticsButton.onClick.RemoveListener(OnGoToStatisticsButtonClicked);
            _view.ProcessingTypeDropdown.onValueChanged.RemoveListener(OnProcessingTypeSelected);
        }

        public void ShowPanel(IStateMachine stateMachine, SelectedStatisticData selectedStatisticData = null)
        {
            _stateMachine = stateMachine;
            RegisterEvents();

            _selectedStatisticData = selectedStatisticData != null ? selectedStatisticData : new SelectedStatisticData();

            _view.ShowPanel(_selectedStatisticData);
        }
        public void HidePanel()
        {
            AnregisterEvents();

            _view.HidePanel();
        }

        private void OnPartButtonClicked()
        {
            var stateData = CreateStateData(MainMenuTypes.Parts);
            _stateMachine.Enter<StatisticChoiceOfCategoryState, StatisticChoiceOfCategoryStateData>(stateData);
        }

        private void OnToolButtonClicked()
        {
            var stateData = CreateStateData(MainMenuTypes.Tools);
            _stateMachine.Enter<StatisticChoiceOfCategoryState, StatisticChoiceOfCategoryStateData>(stateData);
        }

        private StatisticChoiceOfCategoryStateData CreateStateData(MainMenuTypes menuType)
        {
            var statisticChoiceOfCategoryStateData = new StatisticChoiceOfCategoryStateData
                (
                    menuType,
                    _saveloadDataService.GetTypesOfItemsListByType(menuType),
                    _selectedStatisticData
                );

            return statisticChoiceOfCategoryStateData;
        }

        private void OnProcessingTypeSelected(int index)
        {
            var processingType = (ProcessingType)index;
            SetProcessingType(processingType);
            _view.ShowToolSelection();
        }

        public void SetProcessingType(ProcessingType processingType)
        {
            _selectedStatisticData.selectedProcessingType = processingType;
        }

        private void OnGoToStatisticsButtonClicked()
        {
            //if (_service.HasValidStatistics())
            //{
            //    var statistics = _service.GetCurrentStatistics();
            //    _view.ShowStatisticsButtons();
            //}
            //else
            //{
            //    // Show error message
            //}
        }

        public bool HasValidStatistics()
        {
            return _selectedStatisticData.selectedPart != null && _selectedStatisticData.selectedTool != null && _selectedStatisticData.selectedPart.Statistic != null &&
                   _selectedStatisticData.selectedPart.Statistic.Exists(stat =>
                       stat.ProcessingType == _selectedStatisticData.selectedProcessingType && stat.Tool == _selectedStatisticData.selectedTool);
        }

        public Statistic GetCurrentStatistics()
        {
            if (!HasValidStatistics())
            {
                return null;
            }

            return _selectedStatisticData.selectedPart.Statistic.Find(stat =>
                stat.ProcessingType == _selectedStatisticData.selectedProcessingType && stat.Tool == _selectedStatisticData.selectedTool);
        }
    }
}