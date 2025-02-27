using Scripts.Data;
using Scripts.MyTools;
using Scripts.UI.Markers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Services
{
    public class ChoiceOfCategoryService : IChoiceOfCategoryService
    {
        private readonly ISaveloadDataService _saveloadDataService;
        private readonly IButtonCreator _buttonCreator;

        private readonly Transform _panel;
        private readonly Text _sectionNameText;
        private readonly Transform _content;


        private UnityEvent<MainMenuTypes,int> _choiceButtonPressed;

        private List<string> _selectedCategories = new List<string>();
        private List<StatisticData> _selectedStatisticList = new List<StatisticData>();
        private StatisticData _selectedStatisticData = new StatisticData();

        private MainMenuTypes _menuType;

        public ChoiceOfCategoryService
        (
            ISaveloadDataService saveloadDataService,
            IButtonCreator buttonCreator,
            ChoiceOfCategoryElements choiceOfCategoryElements          
        )
        {
            _panel = choiceOfCategoryElements.panel;
            _sectionNameText = choiceOfCategoryElements.sectionNameText;
            _content = choiceOfCategoryElements.content;

            _buttonCreator = buttonCreator;

            _saveloadDataService = saveloadDataService;            
        }
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
                GameObject.Destroy(item.gameObject);
            }
        }
        public void Create(List<string> list, MainMenuTypes menuType, UnityEvent<MainMenuTypes,int> choiceButtonPresed)
        {
            _menuType = menuType;

            _selectedCategories = list;

            _sectionNameText.text = menuType.ToString();

            Clear();

            _choiceButtonPressed = choiceButtonPresed;

            var buttons = _buttonCreator.Create(list.ToArray(), _content);

            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;
                var myButton = buttons[index].GetComponent<Button>();
                myButton.onClick.AddListener(delegate { ButtonPressed(index); });
                var buttonText = myButton.GetComponentInChildren<Text>();
                var count = _saveloadDataService.GetItemsCount(menuType, list[i]);
                if(menuType != MainMenuTypes.Statistic)
                    buttonText.text = $"{buttonText.text} - ({count})";   
                else
                    buttonText.text = $"{buttonText.text}";
            }
        }
        
        public void ButtonPressed(int indexOfSelectedCategoty)
        {     
            _choiceButtonPressed?.Invoke(_menuType, indexOfSelectedCategoty);                       
        }
    }
}