using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.UI.Markers;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticService : IChoiceOfStatisticService
    {
        private SelectedStatistic _selectedStatisticData;

        private IStateMachine _stateMachine;
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly IPopUpMassageService _popUpMassageService;
        private readonly IConfirmPanelService _confirmPanelService;
        private readonly IStatisticsInputService _statisticsInputService;
        private readonly ChoiceOfStatisticDataView _view;

        public ChoiceOfStatisticService
         (
            ISaveloadDataService saveloadDataService,
            IPopUpMassageService popUpMassageService,
            IConfirmPanelService confirmPanelService,
            IStatisticsInputService statisticsInputService,
            StatisticViewElements statisticViewElements
        )
        {
            _saveloadDataService = saveloadDataService;
            _popUpMassageService = popUpMassageService;
            _confirmPanelService = confirmPanelService;
            _statisticsInputService = statisticsInputService;
            _view = new ChoiceOfStatisticDataView(statisticViewElements);
        }
        private void RegisterEvents()
        {
            _view.SelectPartButton.onClick.AddListener(OnPartButtonClicked);
            _view.ProcessingTypeDropdown.onValueChanged.AddListener(OnProcessingTypeSelected);
            _view.SelectToolButton.onClick.AddListener(OnToolButtonClicked);

            _view.GoToStatisticsButton.onClick.AddListener(OnGoToStatisticsButtonClicked);
            _view.EditStatisticsButton.onClick.AddListener(EditStatisticsButtonClicked);
        }

        private void AnregisterEvents()
        {
            _view.SelectPartButton.onClick.RemoveListener(OnPartButtonClicked);
            _view.SelectToolButton.onClick.RemoveListener(OnToolButtonClicked);
            _view.GoToStatisticsButton.onClick.RemoveListener(OnGoToStatisticsButtonClicked);
            _view.ProcessingTypeDropdown.onValueChanged.RemoveListener(OnProcessingTypeSelected);
        }

        public void ShowPanel(IStateMachine stateMachine, SelectedStatistic selectedStatisticData = null)
        {
            _stateMachine = stateMachine;
            RegisterEvents();

            _selectedStatisticData = selectedStatisticData != null ? selectedStatisticData : new SelectedStatistic();

            _view.Initialize();

            _view.HideStatisticsButtons();
            CheckSelectedData(_selectedStatisticData);
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
            _stateMachine.Enter<StatisticSelectionChoiceOfCategoryState, StatisticChoiceOfCategoryStateData>(stateData);
        }

        private void OnToolButtonClicked()
        {
            var stateData = CreateStateData(MainMenuTypes.Tools);
            _stateMachine.Enter<StatisticSelectionChoiceOfCategoryState, StatisticChoiceOfCategoryStateData>(stateData);
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
        }

        public void SetProcessingType(ProcessingType processingType)
        {
            _selectedStatisticData.selectedProcessingType = processingType;
            CheckSelectedData(_selectedStatisticData);
        }

        private void CheckSelectedData(SelectedStatistic selectedStatisticData)
        {
            if (selectedStatisticData.selectedPart == null)
            {
                _view.ShowPartSelection("Select part");
            }
            else if (selectedStatisticData.selectedPart != null)
            {
                var text = $"Selected part: {selectedStatisticData.selectedPart.Id} {selectedStatisticData.selectedPart.Name}";

                _view.ShowPartSelection(text);

                List<string> processingTypes = new List<string>();

                foreach (var type in System.Enum.GetValues(typeof(ProcessingType)))
                {
                    processingTypes.Add(type.ToString());
                }

                _view.ShowProcessingTypeOptions();
            }

            if (selectedStatisticData.selectedPart != null &&
                selectedStatisticData.selectedProcessingType != ProcessingType.NotSpecified &&
                selectedStatisticData.selectedTool == null)
            {
                _view.ShowToolSelection("Select tool");
            }
            else if (selectedStatisticData.selectedTool != null)
            {
                var text = $"Selected tool: {selectedStatisticData.selectedTool.Id} {selectedStatisticData.selectedTool.Name}";
                _view.ShowToolSelection(text);
                _view.ShowStatisticsButtons();
            }
        }

        private void OnGoToStatisticsButtonClicked()
        {
            if (HasValidStatistics())
            {
                var statistics = GetCurrentStatistics();
            }
            else
                _popUpMassageService.Show("Statistic not found");
        }
        private void EditStatisticsButtonClicked()
        {
            if (HasValidStatistics())
            {
                var lastStateData = CreateStateData(MainMenuTypes.Statistic);

                var stateData = new ChoiceOfStatisticDataStateData(lastStateData.menuType, null, lastStateData.selectedStatistic, GetCurrentStatistics());
                _stateMachine.Enter<ChoiceOfStatisticDataState, ChoiceOfStatisticDataStateData>(stateData);
            }
            else
            {
                _confirmPanelService.Show(CreateStatistic);
            }
        }        
        public bool HasValidStatistics()
        {
            return _selectedStatisticData.selectedPart != null &&
                   _selectedStatisticData.selectedTool != null &&                   
                   GetCurrentStatistics() != null;
        }

        public Statistic GetCurrentStatistics()
        {
            var statistics = _selectedStatisticData.selectedPart.Statistic;
            var processingType = _selectedStatisticData.selectedProcessingType;
            var tool = _selectedStatisticData.selectedTool;                   

            return statistics.FirstOrDefault(item => item.Equals(tool) && item.ProcessingType == processingType);
        }
        private void CreateStatistic()
        {
            var statistics = _selectedStatisticData.selectedPart.Statistic;

            statistics.Add(new Statistic(_selectedStatisticData.selectedTool, _selectedStatisticData.selectedProcessingType));

            EditStatisticsButtonClicked();
        }
    }
}