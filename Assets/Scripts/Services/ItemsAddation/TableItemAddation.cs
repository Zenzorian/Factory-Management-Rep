using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public class TableItemAddation : BaseAddition, IItemAddationService
    {       
        private Dictionary<string, InputField> _inputFields;

        public TableItemAddation
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

            var elementType = addationData.categoryName;            
            _addButton.onClick.AddListener(Addation);
            _inputFields = BuildAdditionPanel(typeof(TableItem), elementType);
        }

        private void Addation()
        {
            ValidateAndCreate(_inputFields);          
        }
        public async void ValidateAndCreate(Dictionary<string, InputField> inputFields)
        {
            int? id = await _validator.ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await _validator.ValidateStringInput(inputFields["Name"]);
            if (name == null) return;

            TableItem newItem = new TableItem(
                id: id.Value,
                name: name,
                type: inputFields["Type"].text
            );

            _saveloadDataService.AddItemInTable(_menuType, newItem);

            Added();
        }
    }

}