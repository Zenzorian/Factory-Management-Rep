using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Scripts.Data;
using Scripts;
using Scripts.Data.Tools;
using Scripts.Services;

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

        //ConfirmationPanelService.instance.OnConfirmed.AddListener(OnConfirmation);

    }

    private void PartButtonClicked()
    {
        MenuManager.Instance.OpenMenu((int)MainMenuTypes.StatisticPart);
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
        MenuManager.Instance.OpenMenu((int)MainMenuTypes.StatisticTool);
    }
    public void OnToolSelected(Tool tool)
    {
        _selectedTool = tool;
        _toolText.text = $"Selected Tool: {_selectedTool.Name}";
        _goToStatisticsButton.gameObject.SetActive(true);
        _statisticsEditingButton.gameObject.SetActive(true);
    }
    private Statistic Check()
    {
        if (_selectedPart.Statistic.Count == 0 || _selectedPart.Statistic == null) return null;       
       
        foreach (var item in _selectedPart.Statistic)
        {
            if (item.ProcessingType == _selectedProcessingType && item.Tool.Name == _selectedTool.Name)
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
            var item = Check();

            if(item.Data.Count < 4)
            {
                //PopupMessageService.instance.Show("Insufficient data. Complete the statistics");
                return;
            }
            else OpenCurrentStatistic(item);
            _menuConvas.gameObject.SetActive(false);
            _statisticConvas.gameObject.SetActive(true);
        }
    }   
    private void OpenConfirmationAndAddationMenu()
    {
       // MenuManager.Instance.ShowConfirmationPanel();
    }
    private void OpenCurrentStatistic(Statistic statistics)
    {
        _statisticsGraphView.Init(statistics);
    }
    private void OnConfirmation()
    {
        _selectedPart.Statistic.Add(new Statistic(_selectedTool, _selectedProcessingType));        
    }
    private void OnStatisticsEditingButtonClicked()
    {
        if (Check() == null)
        { 
            OpenConfirmationAndAddationMenu();
            return;
        }       
        CurrentStatisticDataList = Check().Data;
        MenuManager.Instance.OpenStatisticChoiceCategory(CurrentStatisticDataList);
    }
    public void CloseStatistic()
    {
        _menuConvas.gameObject.SetActive(true);
        _statisticConvas.gameObject.SetActive(true);
    }
}
