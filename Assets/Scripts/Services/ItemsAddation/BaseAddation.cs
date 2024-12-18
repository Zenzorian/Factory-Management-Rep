using Scripts.Infrastructure.AssetManagement;
using Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public abstract class BaseAddition
    {
        protected Transform _addationPanel;
        protected Transform _content;
        protected InputFieldCreator _inputFieldCreator;       
        protected InputField _inputField;        
        protected InputFieldValidator _validator = new InputFieldValidator();
        protected Button _addButton;
        protected Button _closeButton;

        protected GlobalUIElements _globalUIElements;
        protected ISaveloadDataService _saveloadDataService;

        protected MainMenuTypes _menuType;

        private Action _onAdded;
        public BaseAddition
        (
            ISaveloadDataService saveloadDataService,
            ItemsAddationViewElements itemsAddationViewElements,
            GlobalUIElements globalUIElements
        )
        {
            _inputFieldCreator = itemsAddationViewElements.inputFieldCreator;
            _content = itemsAddationViewElements.content;           
            _addButton = itemsAddationViewElements.addButton;
            _closeButton = itemsAddationViewElements.closeButton;
            _addationPanel = itemsAddationViewElements.addationPanel;
            _globalUIElements = globalUIElements;
            _saveloadDataService = saveloadDataService;

            _closeButton.onClick.AddListener(Close);
        }
        public void Initialize(MainMenuTypes menuType, Action onAdded)
        {    
            _addationPanel.gameObject.SetActive(true);
            _globalUIElements.addationButton.gameObject.SetActive(false);
            _globalUIElements.backButton.gameObject.SetActive(false);

            _menuType = menuType;
            _onAdded = onAdded;
        }
        public void Close()
        {
            _addationPanel.gameObject.SetActive(false);
            _globalUIElements.addationButton.gameObject.SetActive(true);
            _globalUIElements.backButton.gameObject.SetActive(true);
           
            _addButton.onClick.RemoveAllListeners();
        }
        public void Added()
        {   
            Close();
            _onAdded?.Invoke();
        }
        public Dictionary<string, InputField> BuildAdditionPanel(Type type, string elementType = null)
        {
            Clear();            
            var inputFields = _inputFieldCreator.Create(type, _content,elementType);
            return inputFields;
        }
        
        public virtual void Clear()
        {
            foreach (Transform item in _content)
            {
                GameObject.Destroy(item.gameObject);
            }
        }  
    }    
}
