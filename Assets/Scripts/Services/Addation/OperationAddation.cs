using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.UI.Markers;
using System;

namespace Scripts.Services
{
    [System.Serializable]
    public class OperationAddation : BaseAddition, IItemAddationService
    {      
        private Part _part;
        public OperationAddation
        (
            ISaveloadDataService saveloadDataService,
            ItemsAddationViewElements itemsAddationViewElements,
            GlobalUIElements globalUIElements
        ) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
            
        }

        public void Open(AddationData addationData, Action onAdded)
        {
            _part = addationData.part;
            Initialize(addationData.menuType, onAdded);

            _addButton.onClick.AddListener(AddToList);
            
            Clear();
            string title = "Set the name of a new operation";
            _inputField = _inputFieldCreator.Create(title, _content);
        }

        public async void AddToList()
        {
            string name = await _validator.ValidateStringInput(_inputField);
            if (name == null) return;

            _saveloadDataService.AddOperation(_part, _inputField.text);   
            
            Added();
        }       
    }
}
