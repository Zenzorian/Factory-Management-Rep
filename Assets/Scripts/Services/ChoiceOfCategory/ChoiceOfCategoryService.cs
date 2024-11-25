using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public class ChoiceOfCategoryService : MonoBehaviour , IChoiceOfCategoryService
    {
        private readonly Transform _panel;
        private readonly Text _sectionNameText;        
        private readonly Transform _content;

        private readonly ButtonCreator _buttonCreator;

        private List<string> _selectedCategories = new List<string>();
        private List<StatisticData> _selectedStatisticList = new List<StatisticData>();
        private StatisticData _selectedStatisticData = new StatisticData();

        public ChoiceOfCategoryService(ChoiceOfCategoryElements choiceOfCategoryElements, ButtonCreator buttonCreator)
        {
            _panel = choiceOfCategoryElements.panel;
            _sectionNameText = choiceOfCategoryElements.sectionNameText;           
            _content = choiceOfCategoryElements.content;

            _buttonCreator = buttonCreator;
        }

        public static MainMenuTypes MenuType { get; private set; }
        public void Activate()
        {
            _panel.gameObject.SetActive(true);
        }
        public void Deactivate()
        {
            Clear();
            _panel.gameObject.SetActive(false);
        }
        private void Clear() 
        {
            foreach (Transform item in _content)
            {
                Destroy(item.gameObject);
            }
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
        }
        public void CreateForStatistic(List<StatisticData> list)
        {    
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
    }
}