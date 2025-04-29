using Scripts.Infrastructure.States;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Data;
using System.Collections.Generic;

namespace Scripts.Services.Statistics
{
    public class ChoiceOfStatisticService : IChoiceOfStatisticService
    {
        private SelectedStatisticsContext _selectedStatisticData;

        private IStateMachine _stateMachine;
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly IPopUpService _popUpService;
        private readonly ITableProcessorService _tableProcessorService;
        private readonly ChoiceOfStatisticDataView _view;
        private readonly IElementsProvider _elementsProvider;

        private OperationAddation _operationAddation;
        private ToolStatisticAddation _toolStatisticAddation;

        private bool _isToolStartSelection = false;

        public ChoiceOfStatisticService
         (
            ISaveloadDataService saveloadDataService,
            IPopUpService popUpService,
            ITableProcessorService tableProcessorService,
            IElementsProvider elementsProvider            
        )
        {
            _saveloadDataService = saveloadDataService;
            _popUpService = popUpService;
            _tableProcessorService = tableProcessorService;
            _elementsProvider = elementsProvider;

            _view = new ChoiceOfStatisticDataView(elementsProvider.StatisticViewElements, _tableProcessorService);
            _operationAddation = new OperationAddation(saveloadDataService, elementsProvider.ItemsAddationViewElements, elementsProvider.GlobalUIElements);
            _toolStatisticAddation = new ToolStatisticAddation(saveloadDataService, elementsProvider.ItemsAddationViewElements, elementsProvider.GlobalUIElements);
        }
        private void RegisterEvents()
        {
            _view.SelectPartButton.onClick.AddListener(OnPartButtonClicked);
           
            _elementsProvider.GlobalUIElements.editButton.onClick.AddListener(_view.OnEditMode);
        }

        private void AnregisterEvents()
        {
            _view.SelectPartButton.onClick.RemoveAllListeners();

            _view.GoToStatisticsButton.onClick.RemoveAllListeners();
            _elementsProvider.GlobalUIElements.editButton.onClick.RemoveAllListeners();
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
            _selectedStatisticData = null;          
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
                _isToolStartSelection = false;
            }
            else
            {
                var text = $"Selected part: {selectedStatisticData.selectedPart.Id} {selectedStatisticData.selectedPart.Name}";

                _view.ShowPartSelection(text);

                var statisticTableActions = new StatisticTableActions
                (
                    OnAddOperationButtonClicked,
                    OnAddToolButtonClicked,
                    OnStatisticsSelected,
                    OnDeleteOperation,
                    OnDeleteStatistic
                );

                _view.ShowOperations(selectedStatisticData.selectedPart, statisticTableActions);         
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

        private void OnStatisticsSelected(PartCardData partCardData)
        {            
            if (partCardData.statistic.Data == null)
            { 
                _popUpService.ShowMessageAutoClose("Statistic not found", MessageType.warning);

                // var statistic = GetCurrentStatistic();

                // var statisticGrafViewStateData = new StatisticGrafViewStateData(statistic, _selectedStatisticData);
                // _stateMachine.Enter<StatisticGrafViewState, StatisticGrafViewStateData>(statisticGrafViewStateData);
            }
            else
            {
                var lastStateData = CreateStateData(MainMenuTypes.Statistic);

                var stateData = new ChoiceOfStatisticDataStateData(lastStateData.menuType, null, lastStateData.selectedStatistic, partCardData.statistic);
                _stateMachine.Enter<ChoiceOfStatisticDataState, ChoiceOfStatisticDataStateData>(stateData);
            }                
        }
        private void OnDeleteOperation(PartCardData partCardData)
        {           
            _popUpService.ShowConfirm("Are you sure you want to delete this operation?", 
                () => DeleteOperation(partCardData));
        }
        private void DeleteOperation(PartCardData partCardData)
        {
            _saveloadDataService.DeleteOperation(partCardData.part, partCardData.operation.Name);
            CheckSelectedData(_selectedStatisticData);
        }
        
        private void OnDeleteStatistic(PartCardData partCardData)
        { 
            _popUpService.ShowConfirm("Are you sure you want to delete this statistic?", 
                    () => DeleteStatistic(partCardData));
        }
        private void DeleteStatistic(PartCardData partCardData)
        {           
            _saveloadDataService.DeleteStatistic
            (
                partCardData.part,
                partCardData.operation,
                partCardData.statistic.Tool,
                partCardData.statistic.ProcessingType
            );
           CheckSelectedData(_selectedStatisticData);
        }
    }
}