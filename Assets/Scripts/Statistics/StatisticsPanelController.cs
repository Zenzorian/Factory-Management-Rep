using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FactoryManager.Data;
using FactoryManager;
using FactoryManager.Data.Tools;

public class StatisticsPanelController : MonoBehaviour
{
    [SerializeField] private GlobalData _globalData;

    [SerializeField] private Transform _menuConvas;
    [SerializeField] private Transform _statisticConvas;

    [SerializeField] private StatisticsGraphView _statisticsGraphView;

    [SerializeField] private Text _partText;
    [SerializeField] private Button _selectPartButton;
    [SerializeField] private Text _processingTypeText;
    [SerializeField] private Dropdown _processingTypeDropdown;
    [SerializeField] private Text _toolText;
    [SerializeField] private Button _selectToolButton;
    [SerializeField] private Button _goToStatisticsButton;
    [SerializeField] private Button _statisticsEditingButton;
    
    private Part _selectedPart;
    private Tool _selectedTool;
    private ProcessingType _selectedProcessingType;

    public static List<StatisticData> CurrentStatisticDataList { get;set;}
    private void Start()
    {
        _partText.text = "Part";
        _selectPartButton.GetComponentInChildren<Text>().text = "Select Part";

        _processingTypeText.text = "Select Processing Type";

        _toolText.text = "Tool";
        _selectToolButton.GetComponentInChildren<Text>().text = "Select Tool";
        _goToStatisticsButton.GetComponentInChildren<Text>().text = "Go to Statistics";

        _selectPartButton.onClick.AddListener(PartButtonClicked);
        _selectToolButton.onClick.AddListener(ToolButtonClicked);
        _statisticsEditingButton.onClick.AddListener(OnStatisticsEditingButtonClicked);
        _goToStatisticsButton.onClick.AddListener(OnGoToStatisticsButtonClicked);

        _processingTypeText.gameObject.SetActive(false);
        _processingTypeDropdown.gameObject.SetActive(false);
        _toolText.gameObject.SetActive(false);
        _selectToolButton.gameObject.SetActive(false);
        _goToStatisticsButton.gameObject.SetActive(false);
        _statisticsEditingButton.gameObject.SetActive(false);

        ConfirmationPanel.instance.OnConfirmed.AddListener(OnConfirmation);

    }

    private void PartButtonClicked()
    {
        MenuManager.instance.OpenMenu((int)MainMenuTypes.StatisticPart);
    }
    public void OnPartSelected(Part part)
    {
        _selectedPart = part;
        _partText.text = $"Selected Part: {_selectedPart.Name}";
        ShowProcessingTypeDropdown();
        
    }

    private void ShowProcessingTypeDropdown()
    {
        _processingTypeDropdown.gameObject.SetActive(true);
        _processingTypeText.gameObject.SetActive(true);
        _processingTypeDropdown.ClearOptions();
        List<string> processingTypes = new List<string>();

        foreach (var type in System.Enum.GetValues(typeof(ProcessingType)))
        {
            processingTypes.Add(type.ToString());
        }

        _processingTypeDropdown.AddOptions(processingTypes);
        _processingTypeDropdown.onValueChanged.AddListener(OnProcessingTypeSelected);
    }

    private void OnProcessingTypeSelected(int index)
    {
        _selectedProcessingType = (ProcessingType)index;
        _toolText.gameObject.SetActive(true);
        _selectToolButton.gameObject.SetActive(true);
    }

    private void ToolButtonClicked()
    {
        MenuManager.instance.OpenMenu((int)MainMenuTypes.StatisticTool);
    }
    public void OnToolSelected(Tool tool)
    {
        _selectedTool = tool;
        _toolText.text = $"Selected Tool: {_selectedTool.Marking}";
        _goToStatisticsButton.gameObject.SetActive(true);
        _statisticsEditingButton.gameObject.SetActive(true);
    }
    private Statistics Check()
    {
        if (_selectedPart.Statistics.Count == 0 || _selectedPart.Statistics == null) return null;       
       
        foreach (var item in _selectedPart.Statistics)
        {
            if (item.ProcessingType == _selectedProcessingType && item.Tool.Marking == _selectedTool.Marking)
            {                
                return item;                    
            }
        }
        
        return null;
    }
    private void OnGoToStatisticsButtonClicked()
    {
        if (Check() == null) OpenConfirmationAndAddationMenu();
        else { 
            OpenCurrentStatistic(Check());
            _menuConvas.gameObject.SetActive(false);
            _statisticConvas.gameObject.SetActive(true);
        }
    }   
    private void OpenConfirmationAndAddationMenu()
    {
        MenuManager.instance.ShowConfirmationPanel();
    }
    private void OpenCurrentStatistic(Statistics statistics)
    {
        _statisticsGraphView.Init(statistics);
    }
    private void OnConfirmation()
    {
        _selectedPart.Statistics.Add(new Statistics(_selectedTool, _selectedProcessingType));        
    }
    private void OnStatisticsEditingButtonClicked()
    {
        if (Check() == null)
        { 
            OpenConfirmationAndAddationMenu();
            return;
        }       
        CurrentStatisticDataList = Check().Data;
        MenuManager.instance.OpenStatisticChoiceCategory(CurrentStatisticDataList);
    }
    public void CloseStatistic()
    {
        _menuConvas.gameObject.SetActive(true);
        _statisticConvas.gameObject.SetActive(true);
    }
}
