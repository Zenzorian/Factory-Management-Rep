using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public class ChoiceOfCategoryService : MonoBehaviour , IChoiceOfCategoryService
    {
        [SerializeField] private Text _sectionNameText;
        [SerializeField] private Transform _addButton;         
        [SerializeField] private ButtonCreator _buttonCreator;
        [SerializeField] private Transform _content;

        private List<string> _selectedCategories = new List<string>();
        private List<StatisticData> _selectedStatisticList = new List<StatisticData>();
        private StatisticData _selectedStatisticData = new StatisticData();
        public static MainMenuTypes MenuType { get; private set; }
        private void Awake()
        {            
            AddationManager.instance.OnAdded.AddListener(SomethingAdded);
        }       
        private void SomethingAdded() 
        {
            if (MenuManager.Instance.menuType == MainMenuTypes.StatisticTool)
            {
                CreateForStatistic(_selectedStatisticList);
                return;
            }            
           
            Create(_selectedCategories, MenuType);
        }
        public void Create(List<string> list,MainMenuTypes menuType)
        {
            _selectedCategories = list;
            MenuType = menuType;

            _sectionNameText.text = menuType.ToString();

            Clear();
             
            var buttons = _buttonCreator.Create(list.ToArray(), _content);

            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;
                var myButton = buttons[index].GetComponent<Button>();
                myButton.onClick.AddListener(delegate { ButtonPressed(index); });
                var buttonText = myButton.GetComponentInChildren<Text>();
                //var count = DataManager.Instance.GetItemsCount(menuType, list[i]);
                //buttonText.text = $"{buttonText.text} - ({count})";
                //if((menuType == MainMenuTypes.StatisticTool||
                //menuType == MainMenuTypes.StatisticPart)&& count == 0)
                //myButton.gameObject.SetActive(false);                
            }
           
            if(menuType == MainMenuTypes.StatisticTool||
                menuType == MainMenuTypes.StatisticPart)
                _addButton.gameObject.SetActive(false);
            else _addButton.gameObject.SetActive(true);
        }
        public void CreateForStatistic(List<StatisticData> list)
        {
            _addButton.gameObject.SetActive(true);

            _selectedStatisticList = list;

            Clear();

            string[] names = new string[list.Count];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = $"F = {list[i].F} V = {list[i].V}";
            }

            var buttons = _buttonCreator.Create(names, _content);

            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;
                var myButton = buttons[index].GetComponent<Button>();
                var data = new StatisticData();
                data  = list[i];
                myButton.onClick.AddListener(delegate { MenuManager.Instance.OpenStatisticInputPanel(data);});
            }
        }
        
        public void ButtonPressed(int index)
        {
            //_tableController.OpenTableWithFilter(MenuType, index);             
        }
        private void Clear() 
        {
            foreach (Transform item in _content)
            {
                Destroy(item.gameObject);
            }
        }       
    }
}