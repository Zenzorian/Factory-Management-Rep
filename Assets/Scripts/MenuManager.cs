using FactoryManager.Data;
using FactoryManager.Data.Tools;
using UnityEngine;
namespace FactoryManager
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Transform _mainMenu;

        [SerializeField] private Transform _addationMenu;

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
            var menuType = (MainMenuButtons)value;

            switch (menuType)
            {
                case MainMenuButtons.Workspace:

                    break;
                case MainMenuButtons.Tools:
                    _categoryMenu.Create(typeof(MachineTool));
                    Forwards(_choicePanel.gameObject);
                    break;
                case MainMenuButtons.Workers:
                    _categoryMenu.Create(typeof(FactoryWorker));
                    Forwards(_choicePanel.gameObject);
                    break;
                case MainMenuButtons.Parts:
                    break;
                case MainMenuButtons.Options:
                    Forwards(_optionsPanel.gameObject);
                    break;
                default:
                    break;
            }
        }
        public void OpenAddationMenu()
        {
            Forwards(_addationMenu.gameObject);
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
    public enum MainMenuButtons
    {
        Workspace,
        Tools,
        Workers,
        Parts,
        Options = 99
    }
}