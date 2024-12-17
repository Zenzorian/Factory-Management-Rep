using UnityEngine.UI;
using UnityEngine;
using Scripts.Data;
using System.Collections.Generic;
using Scripts.Services;
using Scripts.Infrastructure.AssetManagement;

namespace Scripts
{
    [System.Serializable]
    public class ToolForm : BaseAddition
    {
        private Tool _tool;

        private Dictionary<string, InputField> _inputFields;
        public ToolForm(ISaveloadDataService saveloadDataService, ItemsAddationViewElements itemsAddationViewElements, GlobalUIElements globalUIElements) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
            _inputFields = BuildAdditionPanel(typeof(Tool));
            itemsAddationViewElements.addButton.onClick.AddListener(Addation);
        }
        private void Addation()
        {
            ValidateAndCreateTool(_inputFields);
        }
        public void Open(List<Tool> tools, TableItem currentTool)
        {
            var desiredTool = (Tool)currentTool;
            _tool = tools[tools.IndexOf(desiredTool)];

            _inputFields["Id"].text = _tool.Id.ToString();
            _inputFields["Name"].text = _tool.Name;
            _inputFields["Type"].text = _tool.Type;

            _inputFields["Note"].text = _tool.Note.ToString();
        }
        public async void ValidateAndCreateTool(Dictionary<string, InputField> inputFields)
        { 
            int? id = await _validator.ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await _validator.ValidateStringInput(inputFields["Name"]);
            if (name == null) return;

            string note = await _validator.ValidateStringInput(inputFields["Note"]);
            if (note == null) return;

            string type = await _validator.ValidateStringInput(inputFields["Type"]);
            if (type == null) return;      
            
            _tool.Id = id.Value;
            _tool.Name = name;
            _tool.Type = type;
            _tool.Note = note;
        }
    }
}
