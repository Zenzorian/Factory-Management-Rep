using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FactoryManager
{
    public class ChoiceOfCategoryMenu : MonoBehaviour
    {
        [SerializeField] private TableController _tableController;
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private ButtonCreator _buttonCreator;

        [SerializeField] private Transform _content;

        private List<string> _selectedCategories = new List<string>();
        public static MainMenuTypes MenuType { get; private set; }
        private void Awake()
        {            
            AddationManager.instance.OnAdded.AddListener(SomethingAdded);
        }
        private void SomethingAdded() 
        {
            Create(_selectedCategories, MenuType);
        }
        public void Create(List<string> list,MainMenuTypes menuType)
        {
            _selectedCategories = list;
            MenuType = menuType;

            Clear();
             
            var buttons = _buttonCreator.Create(list.ToArray(), _content);

            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;
                var myButton = buttons[index].GetComponent<Button>();
                myButton.onClick.AddListener(delegate { ButtonPressed(index); });
            }
        }
        public void ButtonPressed(int index)
        {
            _tableController.OpenTableWithFilter(MenuType, index);
            _menuManager.OpenTableView();
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