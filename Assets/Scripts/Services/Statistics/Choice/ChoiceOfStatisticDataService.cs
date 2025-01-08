using Scripts.Data;
using Scripts.Infrastructure.States;
using Scripts.MyTools;
using Scripts.UI.Markers;
using System.Collections.Generic;
using UnityEditor.Overlays;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticService : IChoiceOfStatisticService
    {
        private SelectedStatistic _selectedStatisticData;

        private IStateMachine _stateMachine;
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly IPopUpMassageService _popUpMassageService;
        private readonly IConfirmPanelService _confirmPanelService;
        
        private readonly ChoiceOfStatisticDataView _view;

        public ChoiceOfStatisticService
         (
            ISaveloadDataService saveloadDataService,
            IPopUpMassageService popUpMassageService,
            IConfirmPanelService confirmPanelService,
            
            StatisticViewElements statisticViewElements
        )
        {
            _saveloadDataService = saveloadDataService;
            _popUpMassageService = popUpMassageService;
            _confirmPanelService = confirmPanelService;           
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
            _view.SelectPartButton.onClick.RemoveAllListeners();
            _view.SelectToolButton.onClick.RemoveAllListeners();
            _view.ProcessingTypeDropdown.onValueChanged.RemoveAllListeners();

            _view.GoToStatisticsButton.onClick.RemoveAllListeners();
            _view.EditStatisticsButton.onClick.RemoveAllListeners();
        }

        public void ShowPanel(IStateMachine stateMachine, SelectedStatistic selectedStatisticData = null)
        {           
            AnregisterEvents();

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
            else
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
                var statistic = GetCurrentStatistic();
                _stateMachine.Enter<StatisticGrafViewState, Statistic>(statistic);
            }
            else
                _popUpMassageService.Show("Statistic not found");
        }
        private void EditStatisticsButtonClicked()
        {
            if (HasValidStatistics())
            {
                var lastStateData = CreateStateData(MainMenuTypes.Statistic);

                var stateData = new ChoiceOfStatisticDataStateData(lastStateData.menuType, null, lastStateData.selectedStatistic, GetCurrentStatistic());
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
                   GetCurrentStatistic() != null;
        }

        public Statistic GetCurrentStatistic()
        {
            List<Statistic> listOfStatistic = _selectedStatisticData.selectedPart.Statistic;
            ProcessingType processingType = _selectedStatisticData.selectedProcessingType;
            Tool tool = _selectedStatisticData.selectedTool;

            Statistic statistick = listOfStatistic.Find(item => item.Tool.Equals(tool) && item.ProcessingType == processingType);
             
            return statistick;
        }
        private void CreateStatistic()
        {
            var statistics = _selectedStatisticData.selectedPart.Statistic;

            statistics.Add(new Statistic(_selectedStatisticData.selectedTool, _selectedStatisticData.selectedProcessingType));

            EditStatisticsButtonClicked();
        }
    }
}