using Scripts.Infrastructure.States;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Data;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticService : IChoiceOfStatisticService
    {
        private SelectedStatisticsContext _selectedStatisticData;

        private IStateMachine _stateMachine;
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly IPopUpMassageService _popUpMassageService;
        private readonly IConfirmPanelService _confirmPanelService;
        private readonly ITableProcessorService _tableProcessorService;
        private readonly ChoiceOfStatisticDataView _view;

        private OperationAddation _operationAddation;
        private ToolStatisticAddation _toolStatisticAddation;

        private bool _isToolStartSelection = false;

        public ChoiceOfStatisticService
         (
            ISaveloadDataService saveloadDataService,
            IPopUpMassageService popUpMassageService,
            IConfirmPanelService confirmPanelService,
            ITableProcessorService tableProcessorService,
            IElementsProvider elementsProvider            
        )
        {
            _saveloadDataService = saveloadDataService;
            _popUpMassageService = popUpMassageService;
            _confirmPanelService = confirmPanelService;
            _tableProcessorService = tableProcessorService;

            _view = new ChoiceOfStatisticDataView(elementsProvider.StatisticViewElements, _tableProcessorService);
            _operationAddation = new OperationAddation(saveloadDataService, elementsProvider.ItemsAddationViewElements, elementsProvider.GlobalUIElements);
            _toolStatisticAddation = new ToolStatisticAddation(saveloadDataService, elementsProvider.ItemsAddationViewElements, elementsProvider.GlobalUIElements);
        }
        private void RegisterEvents()
        {
            _view.SelectPartButton.onClick.AddListener(OnPartButtonClicked);            

            //_view.GoToStatisticsButton.onClick.AddListener(OnGoToStatisticsButtonClicked);
            //_view.EditStatisticsButton.onClick.AddListener(EditStatisticsButtonClicked);
        }

        private void AnregisterEvents()
        {
            _view.SelectPartButton.onClick.RemoveAllListeners();

            _view.GoToStatisticsButton.onClick.RemoveAllListeners();
            _view.EditStatisticsButton.onClick.RemoveAllListeners();
        }

        public void ShowPanel(IStateMachine stateMachine, SelectedStatisticsContext selectedStatisticData = null)
        {           
            AnregisterEvents();

            _stateMachine = stateMachine;
            RegisterEvents();

            _selectedStatisticData = selectedStatisticData != null ? selectedStatisticData : new SelectedStatisticsContext();

            _view.Initialize();

            _view.HideStatisticsButtons();
            CheckSelectedData(_selectedStatisticData);
            _view.ShowPanel(_selectedStatisticData);

            if(_selectedStatisticData.selectedPart == null)
            OnPartButtonClicked();         
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
            _isToolStartSelection = true;
            _operationAddation.Close();
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

        private void CheckSelectedData(SelectedStatisticsContext selectedStatisticData)
        {
            if (selectedStatisticData.selectedPart == null)
            {
                _view.ShowPartSelection("Select part");
            }
            else
            {
                var text = $"Selected part: {selectedStatisticData.selectedPart.Id} {selectedStatisticData.selectedPart.Name}";

                _view.ShowPartSelection(text);

                _view.ShowOperations(selectedStatisticData.selectedPart, OnAddOperationButtonClicked, OnAddToolButtonClicked);         
            }   

            if(_isToolStartSelection == true)
            {
                OnAddToolButtonClicked();
            }
        }

        private void OnAddOperationButtonClicked()
        {
            var addationData = new AddationData(MainMenuTypes.Statistic, 0, null, _selectedStatisticData);
            _operationAddation.Open(addationData, () => CheckSelectedData(_selectedStatisticData));
        }

        private void OnAddToolButtonClicked(Operation operation = null)
        {
            if(operation != null)_selectedStatisticData.selectedOperation = operation;
           
            var addationData = new AddationData(MainMenuTypes.Statistic, 0, null, _selectedStatisticData, OnToolButtonClicked);
            _toolStatisticAddation.Open(addationData, () => OnToolAdded(_selectedStatisticData));          
        }
        private void OnToolAdded(SelectedStatisticsContext selectedStatisticData)
        {
            _isToolStartSelection = false;
            CheckSelectedData(selectedStatisticData);
        }

        // private void OnGoToStatisticsButtonClicked()
        // { 
        //     if (HasValidStatistics())
        //     {
        //         var statistic = GetCurrentStatistic();

        //         var statisticGrafViewStateData = new StatisticGrafViewStateData(statistic, _selectedStatisticData);
        //         _stateMachine.Enter<StatisticGrafViewState, StatisticGrafViewStateData>(statisticGrafViewStateData);
        //     }
        //     else
        //         _popUpMassageService.Show("Statistic not found");
        // }
        // private void EditStatisticsButtonClicked()
        // {
        //     if (HasValidStatistics())
        //     {
        //         var lastStateData = CreateStateData(MainMenuTypes.Statistic);

        //         var stateData = new ChoiceOfStatisticDataStateData(lastStateData.menuType, null, lastStateData.selectedStatistic, GetCurrentStatistic());
        //         _stateMachine.Enter<ChoiceOfStatisticDataState, ChoiceOfStatisticDataStateData>(stateData);
        //     }
        //     else
        //     {
        //         _confirmPanelService.Show(CreateStatistic);
        //     }
        // }        
        // public bool HasValidStatistics()
        // {
        //     return _selectedStatisticData.selectedPart != null &&
        //         //    _selectedStatisticData.selectedTool != null &&                   
        //            GetCurrentStatistic() != null;
        // }

        // public Statistic GetCurrentStatistic()
        // {
        //     List<Statistic> listOfStatistic = _selectedStatisticData.selectedPart.Operations.Find(item => item.Statistics.Count > 0).Statistics;
        //    // ProcessingType processingType = _selectedStatisticData.selectedProcessingType;
        //    // Tool tool = _selectedStatisticData.selectedTool;

        //     Statistic statistick = listOfStatistic.Find(item => item.Tool.Equals(tool) && item.ProcessingType == processingType);
             
        //     return statistick;
        // }
        private void CreateStatistic()
        {
           // var statistics = _selectedStatisticData.selectedPart.Statistic;

            //statistics.Add(new Statistic(_selectedStatisticData.selectedTool, _selectedStatisticData.selectedProcessingType));

            //EditStatisticsButtonClicked();
        }
    }
}