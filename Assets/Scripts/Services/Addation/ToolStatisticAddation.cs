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
        private Dropdown _dropdown;
        private Action _onToolButtonClicked;

        private AddationData _addationData;

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
            _onToolButtonClicked = addationData.onToolButtonClicked;
            _addationData = addationData;

            Initialize(addationData.menuType, onAdded);

            _addButton.onClick.AddListener(AddToList);
            
            Clear();
            
            if(addationData.selectedStatistic.selectedTool != null)
            {
                CreateTableText($"Selected tool: {addationData.selectedStatistic.selectedTool.Name}");
                string title = "Edit the tool";
                CreateToolButton(title);

                CreateDropdown();
            }
            else
            {
                CreateTableText("Tool not selected");
                string title = "Select the tool";
                CreateToolButton(title);
            }           
        }

        public async void AddToList()
        {
            if(_addationData.selectedStatistic.selectedTool == null) return;
            var processingType = (ProcessingType)_dropdown.value;
            var part = _addationData.selectedStatistic.selectedPart;
            var operation = _addationData.selectedStatistic.selectedOperation;
            _saveloadDataService.AddStatistic(part, operation.Name, _addationData.selectedStatistic.selectedTool, processingType);   

            Debug.Log("AddToList");

            Added();
        }    
        private void CreateTableText(string lable)
        {
            var lableText = GameObject.Instantiate(_statisticAddationViewElements.lableText, _content);
            lableText.transform.SetParent(_content);
            lableText.GetComponentInChildren<Text>().text = lable;
        }
        private void CreateToolButton(string title)
        {
            var button = GameObject.Instantiate(_statisticAddationViewElements.button, _content);
            button.transform.SetParent(_content);          
            button.GetComponentInChildren<Text>().text = title;    
            button.onClick.AddListener(() => _onToolButtonClicked());
        }
       
        private void CreateDropdown()
        {
           _dropdown = GameObject.Instantiate(_statisticAddationViewElements.dropdown, _content);
           _dropdown.transform.SetParent(_content);
           _dropdown.options.Clear();

           foreach (var processingType in Enum.GetValues(typeof(ProcessingType)))
           {
                _dropdown.options.Add(new Dropdown.OptionData(processingType.ToString()));
           }            
        }        
    }
}
