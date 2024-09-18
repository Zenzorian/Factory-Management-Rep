using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using System.Collections.Generic;
using UnityEngine.Events;

namespace FactoryManager
{
    [System.Serializable]
    public class ToolAddation : BaseAddition
    {
        private Dictionary<string, InputField> _inputFields;
        public ToolAddation(InputFieldCreator inputFieldCreator, Transform content, UnityEvent OnAdded, Button button,string elementType) : base(inputFieldCreator, content, OnAdded, button)
        {
            _inputFields = BuildAdditionPanel(typeof(Tool),elementType);
            button.onClick.AddListener(Addation);
        }
        private void Addation()
        {
            ValidateAndCreateTool(_inputFields);
        }

        public async void ValidateAndCreateTool(Dictionary<string, InputField> inputFields)
        { 
            int? id = await ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string marking = await ValidateStringInput(inputFields["Marking"]);
            if (marking == null) return;

            string note = await ValidateStringInput(inputFields["Note"]);
            if (note == null) return;

            string type = await ValidateStringInput(inputFields["Type"]);
            if (type == null) return;

            // Логика для создания объекта Tool
            Tool newTool = new Tool(id.Value, marking, note, type);
            // Вызов метода для добавления инструмента в систему
            DataManager.Instance.AddItem(MainMenuTypes.Tools,newTool);  
            Added();
        }
    }
}
