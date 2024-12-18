using UnityEngine.UI;
using UnityEngine;
using Scripts.Data;
using System.Collections.Generic;
using Scripts.Services;
using Scripts.Infrastructure.AssetManagement;
using Scripts.UI;

namespace Scripts
{
    [System.Serializable]
    public class PartForm : BaseAddition
    {
        private Part _part;
        private Dictionary<string, InputField> _inputFields;
        public PartForm
        (
            ISaveloadDataService saveloadDataService, 
            ItemsAddationViewElements itemsAddationViewElements,
            GlobalUIElements globalUIElements
            ) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
            _inputFields = BuildAdditionPanel(typeof(Part));
            itemsAddationViewElements.addButton.onClick.AddListener(Addation);
            Init(_inputFields);
        }
        private void Addation()
        {
            ValidateAndCreatePart(_inputFields);
        }
        public void Open(List<Part> parts, TableItem currentPart)
        {
            var desiredPart = (Part)currentPart;
            _part = parts[parts.IndexOf(desiredPart)];

            _inputFields["Id"].text = _part.Id.ToString();
            _inputFields["Name"].text = _part.Name;
            _inputFields["Type"].text = _part.Type;
        }
        public void Init(Dictionary<string, InputField> inputFields)
        {              
            _inputFields = inputFields;
            var toolInput = inputFields["Statistic"];
            toolInput.interactable = false;
            ColorBlock colorBlock = toolInput.colors;
            colorBlock.disabledColor = Color.white; // Задаем желаемый цвет для состояния "Disabled"
            toolInput.colors = colorBlock;
            toolInput.text = "Statistic";            
            var selectedListener= toolInput.gameObject.AddComponent<InputFieldSelectListener>();
            selectedListener.onSelectAction = StatisticsClicked;                   
        }
        private void StatisticsClicked()
        {
            //PopupMessageService.instance.Show("Statistical data is set in the Statistics tab");
        }
        public async void ValidateAndCreatePart(Dictionary<string, InputField> inputFields)
        {
            int? id = await _validator.ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await _validator.ValidateStringInput(inputFields["Name"]);
            if (name == null) return;

            string partType = await _validator.ValidateStringInput(inputFields["Type"]);
            if (partType == null) return;

            _part.Id = id.Value;
            _part.Name = name;  
            _part.Type = partType;

        }
    }
}
