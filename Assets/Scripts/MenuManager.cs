using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace FactoryManager
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

        public List<string> TemporaryListOfCategory{private get; set;}
        public string TemporaryTableItemType{private get; set;}
        public List<StatisticData> TemporaryListOfStatisticData{private get; set;}
        public MainMenuTypes menuType;

        public UnityEvent<Part> OnPartSelected;
        public UnityEvent<Tool> OnToolSelected;

        [SerializeField] private GlobalData _globalData;
        [SerializeField] private Transform _mainMenu;

        [SerializeField] private Transform _choicePanel;

        [SerializeField] private Transform _addationPanel;
        [SerializeField] private Transform _tableView;
        [SerializeField] private Transform _optionsPanel;
        [SerializeField] private Transform _statisticPanel;
        [SerializeField] private Transform _statisticInputPanel;
        [SerializeField] private ChoiceOfCategoryMenu _categoryMenu;
        [SerializeField] private AddationManager _addationManager;

        [SerializeField] private StatisticsPanelController _statisticsPanelController;
        [SerializeField] private ConfirmationPanel _confirmationPanel;
        [SerializeField] private StatisticsInputManager _statisticsInputManager;

        [SerializeField] private Button _backButton;

        private GameObject[] _menuStack = new GameObject[50];
        private int _menuIndex = 0;

        public bool statisticPanelIsOpen = false;
        private void Awake()
        {
            Screen.fullScreen = false;
            
            if (instance == null)
                instance = this;

            _menuStack[0] = _mainMenu.gameObject;            
            OnPartSelected.AddListener(PartSelected);                    
        }
        private void Update()
        {
            if(_menuIndex == 0) _backButton.gameObject.SetActive(false);
            else _backButton.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
        public void Back()
        {
            if (_menuIndex == 0) return;

            _menuStack[_menuIndex].SetActive(false);
            _menuIndex--;
            _menuStack[_menuIndex].SetActive(true);
        }
        private void Forwards(GameObject gameObject)
        {
            _menuStack[_menuIndex].SetActive(false);
            _menuIndex++;
            gameObject.SetActive(true);
            _menuStack[_menuIndex] = gameObject;
        }
        public void OpenMenu(int value)
        {
            menuType = (MainMenuTypes)value;
            List<string> selectedList = null;

            if(value >=4) 
            {
                OnToolSelected.RemoveAllListeners();
                OnToolSelected.AddListener(ToolSelected);       
                
                statisticPanelIsOpen = true;
            }
            else statisticPanelIsOpen = false;

            switch (menuType)
            {
                case MainMenuTypes.Workspaces:
                    selectedList = _globalData.typesOfWorkspace;
                    break;
                case MainMenuTypes.Tools:
                    selectedList = _globalData.typesOfTools;
                    break;
                case MainMenuTypes.Workers:
                    selectedList = _globalData.typesOfWorkers;
                    break;
                case MainMenuTypes.Parts:
                    selectedList = _globalData.typesOfParts;
                    break;
                case MainMenuTypes.Statistic:
                    Forwards(_statisticPanel.gameObject);
                    break;
                case MainMenuTypes.StatisticPart:
                    selectedList = _globalData.typesOfParts;
                    break;
                case MainMenuTypes.StatisticTool:
                    selectedList = _globalData.typesOfTools;
                    break;
                case MainMenuTypes.Options:
                    Forwards(_optionsPanel.gameObject);
                    return;
                default:
                    break;
            }

            if (selectedList != null)
            {
                TemporaryListOfCategory = selectedList;
                _categoryMenu.Create(selectedList, menuType);
                Forwards(_choicePanel.gameObject);
            }
        }
        public void OpenListsMenu()
        {
            //Forwards(_listsMenu.gameObject);
        }
        public void OpenAddationPanel(string addationType)
        {
            if(addationType == "Table")
            {
                if(TemporaryTableItemType== null)
                    {
                        Debug.Log("TemporaryListOfTableItem not found");
                        return;
                    }
                    _addationManager.Open(menuType,TemporaryTableItemType);
            }
            else if(addationType == "ChoiceMenu" && menuType == MainMenuTypes.StatisticTool)
            { 
                if(TemporaryListOfStatisticData == null)
                {
                    Debug.Log("TemporaryListOfStatisticData not found");
                    return;
                }
                _addationManager.Open(TemporaryListOfStatisticData);
            } 
            else if(addationType == "ChoiceMenu" && menuType != MainMenuTypes.StatisticTool)
            {
                if(TemporaryListOfCategory == null)
                {
                    Debug.Log("TemporaryListOfCategory not found");
                    return;
                }
                _addationManager.Open(TemporaryListOfCategory);
            }
            
           Forwards(_addationPanel.gameObject);
        }
        public void OpenTableView()
        {
            Forwards(_tableView.gameObject);
        }
        public void OpenProcessingTypeMenu(int index)
        {
            var part = _globalData.listOfParts[index];
            //_categoryMenu.Create(part., MainMenuTypes.Statistic);
            Forwards(_choicePanel.gameObject);
        }
        public void PartSelected(Part part)
        {           
            if (menuType != MainMenuTypes.StatisticPart) return;
            Back();
            Back();

            _statisticsPanelController.OnPartSelected(part);
        }
        public void ToolSelected(Tool tool)
        {
            if (menuType != MainMenuTypes.StatisticTool) return;
            Back();
            Back();
            _statisticsPanelController.OnToolSelected(tool);
        }
        public void ShowConfirmationPanel()
        {
            _confirmationPanel.Show();
        }
        public void OpenStatisticChoiceCategory(List<StatisticData> list)
        { 
            TemporaryListOfStatisticData = list;
            _categoryMenu.CreateForStatistic(list);
                Forwards(_choicePanel.gameObject);        
        }       
        public void OpenStatisticInputPanel(StatisticData data)
        {
            _statisticsInputManager.ShowPanel(data);
            Forwards(_statisticInputPanel.gameObject);
        }
    }
    public enum MainMenuTypes
    {
        Workspaces,
        Tools,
        Workers,
        Parts,
        Statistic,
        Options = 99,
        StatisticPart,
        StatisticTool
    }
}