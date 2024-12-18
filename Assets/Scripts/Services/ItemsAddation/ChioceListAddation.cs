using Scripts.Infrastructure.AssetManagement;
using Scripts.UI;
using System;

namespace Scripts.Services
{
    [System.Serializable]
    public class ChioceListAddation : BaseAddition, IItemAddationService
    {       
        public ChioceListAddation
        (
            ISaveloadDataService saveloadDataService,
            ItemsAddationViewElements itemsAddationViewElements,
            GlobalUIElements globalUIElements
        ) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
            
        }

        public void Open(AddationData addationData, Action onAdded)
        {
            Initialize(addationData.menuType, onAdded);

            _addButton.onClick.AddListener(AddToList);
            
            Clear();
            string title = "Set the name of a new category";
            _inputField = _inputFieldCreator.Create(title, _content);
        }

        public async void AddToList()
        {
            string name = await _validator.ValidateStringInput(_inputField);
            if (name == null) return;

            _saveloadDataService.AddItemInCategory(_menuType, _inputField.text);   
            
            Added();
        }       
    }
}
