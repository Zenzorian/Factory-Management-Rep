using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts
{
    public class MenuManager : MonoBehaviour
    {
        private static MenuManager _instance;        
        public static MenuManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<MenuManager>();
                    if (_instance == null)
                    {
                        var go = new GameObject("DataManager");
                        _instance = go.AddComponent<MenuManager>();
                    }
                }
                return _instance;
            }
        }
        public List<string> TemporaryListOfCategory{private get; set;}
        public string TemporaryTableItemType{private get; set;}
        public List<StatisticData> TemporaryListOfStatisticData{private get; set;}
        public MainMenuTypes menuType;

        public UnityEvent<Part> OnPartSelected;
        public UnityEvent<Tool> OnToolSelected;

        public UnityEvent<TableItem, MainMenuTypes> OnTableItemEdit;

        [SerializeField] private GlobalData _globalData;
        [SerializeField] private Transform _mainMenu;

        [SerializeField] private Transform _choicePanel;

        [SerializeField] private Transform _addationPanel;
        [SerializeField] private Transform _editPanel;
        [SerializeField] private Transform _tableView;
        [SerializeField] private Transform _optionsPanel;
        [SerializeField] private Transform _statisticPanel;
        [SerializeField] private Transform _statisticInputPanel;        
        [SerializeField] private AddationManager _addationManager;

        [SerializeField] private StatisticsPanelController _statisticsPanelController;        
        [SerializeField] private StatisticsInputManager _statisticsInputManager;

        [SerializeField] private Button _backButton;

        private GameObject[] _menuStack = new GameObject[50];
        private int _menuIndex = 0;

        public bool statisticPanelIsOpen = false;
        private void Awake()
        {
            Screen.fullScreen = false;           

            _menuStack[0] = _mainMenu.gameObject;            
            OnPartSelected.AddListener(PartSelected);
            OnTableItemEdit.AddListener(OpenEditPanel);
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
        private void Forward(GameObject gameObject)
        {
            _menuStack[_menuIndex].SetActive(false);
            _menuIndex++;
            gameObject.SetActive(true);
            _menuStack[_menuIndex] = gameObject;
        }
        private void OpenEditPanel(TableItem item,MainMenuTypes menuTypes)
        {
            Forward(_editPanel.gameObject);
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
                    Forward(_statisticPanel.gameObject);
                    break;
                case MainMenuTypes.StatisticPart:
                    selectedList = _globalData.typesOfParts;
                    break;
                case MainMenuTypes.StatisticTool:
                    selectedList = _globalData.typesOfTools;
                    break;
                case MainMenuTypes.Options:
                    Forward(_optionsPanel.gameObject);
                    return;
                default:
                    break;
            }

            if (selectedList != null)
            {
                TemporaryListOfCategory = selectedList;              
                Forward(_choicePanel.gameObject);
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
            
           Forward(_addationPanel.gameObject);
        }
        public void OpenTableView()
        {
            Forward(_tableView.gameObject);
        }
        public void OpenProcessingTypeMenu(int index)
        {
            var part = _globalData.listOfParts[index];
            //_categoryMenu.Create(part., MainMenuTypes.Statistic);
            Forward(_choicePanel.gameObject);
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
        public void OpenStatisticChoiceCategory(List<StatisticData> list)
        { 
            TemporaryListOfStatisticData = list;           
                Forward(_choicePanel.gameObject);        
        }       
        public void OpenStatisticInputPanel(StatisticData data)
        {
            _statisticsInputManager.ShowPanel(data);
            Forward(_statisticInputPanel.gameObject);
        }
    }
    
}