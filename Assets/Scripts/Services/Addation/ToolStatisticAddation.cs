using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.UI.Markers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    [System.Serializable]
    public class ToolStatisticAddation : BaseAddition, IItemAddationService
    {      
        private Operation _operation;
        private Statistic _statistic;
        private Tool _tool;

        private Dropdown _dropdown;

        private ProcessingType _processingType;

        public ToolStatisticAddation
        (
            ISaveloadDataService saveloadDataService,
            ItemsAddationViewElements itemsAddationViewElements,
            GlobalUIElements globalUIElements
        ) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
            
        }

        public void Open(AddationData addationData, Action onAdded)
        {
            _operation = addationData.operation;
            Initialize(addationData.menuType, onAdded);

            _addButton.onClick.AddListener(AddToList);
            
            Clear();
            string title = "Select the tool";
            CreateToolButton(title);
            //_inputField = _inputFieldCreator.Create(title, _content);
        }

        public async void AddToList()
        {
            string name = await _validator.ValidateStringInput(_inputField);
            if (name == null) return;

            //_saveloadDataService.AddOperation(_part, _inputField.text);   
            
            Added();
        }    
        private void CreateToolButton(string title)
        {
            _button = GameObject.Instantiate(_button, _content);
            _button.transform.SetParent(_content);
            _button.onClick.AddListener(ShowTools);
            _button.GetComponentInChildren<Text>().text = title;            
        }
        private void ShowTools()
        {
                        
        }
        private void OnToolSelected(Tool tool)
        {
            _tool = tool;
            CreateDropdown();
        }
       
        private void CreateDropdown()
        {
           _dropdown = GameObject.Instantiate(_dropdown, _content);
           _dropdown.transform.SetParent(_content);
           _dropdown.options.Clear();

           foreach (var processingType in Enum.GetValues(typeof(ProcessingType)))
           {
            _dropdown.options.Add(new Dropdown.OptionData(processingType.ToString()));
           }
           _dropdown.onValueChanged.AddListener((index) => _processingType = (ProcessingType)index);
        }        
    }
}
