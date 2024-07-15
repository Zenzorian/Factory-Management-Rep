using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections.Generic;
using UnityEngine;
namespace FactoryManager
{
    public class MenuManager : MonoBehaviour
    {

        [SerializeField] private GlobalData _globalData;
        [SerializeField] private Transform _mainMenu;       

        [SerializeField] private Transform _choicePanel;

        [SerializeField] private Transform _addationPanel;
        [SerializeField] private Transform _tableView;
        [SerializeField] private Transform _optionsPanel;
        [SerializeField] private ChoiceOfCategoryMenu _categoryMenu;


        private GameObject[] _menuStack = new GameObject[4];
        private int _menuIndex = 0;
        
        private void Awake()
        {
            _menuStack[0] = _mainMenu.gameObject;
            AddationManager.instance.OnAdded.AddListener(Back);            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
        private void Back()
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
            var menuType = (MainMenuTypes)value;
            List<string> selectedList = null;

            switch (menuType)
            {
                case MainMenuTypes.Workspace:
                    selectedList = _globalData.typesOfWorkspaces;
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
                case MainMenuTypes.Options:
                    Forwards(_optionsPanel.gameObject);
                    return;
                default:
                    break;
            }

            if (selectedList != null)
            {
                _categoryMenu.Create(selectedList, menuType);
                Forwards(_choicePanel.gameObject);
            }
        }
        public void OpenListsMenu()
        {
            //Forwards(_listsMenu.gameObject);
        }
        public void OpenAddationPanel()
        {
            Forwards(_addationPanel.gameObject);
        }
        public void OpenTableView()
        {
            Forwards(_tableView.gameObject);
        }
    }
    public enum MainMenuTypes
    {
        Workspace,
        Tools,
        Workers,
        Parts,
        Options = 99
    }
}