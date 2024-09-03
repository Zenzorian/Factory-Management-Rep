using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactoryManager
{
    [System.Serializable]
    public class ToolAddation : BaseAddition
    {
        public async Task<bool> ValidateAndCreateTool(Dictionary<string, InputField> inputFields)
        {
            string marking = await ValidateStringInput(inputFields["Marking"]);
            if (marking == null) return false;

            string note = await ValidateStringInput(inputFields["Note"]);
            if (note == null) return false;

            string type = await ValidateStringInput(inputFields["Type"]);
            if (type == null) return false;

            // Логика для создания объекта Tool
            Tool newTool = new Tool(marking, note, type);
            // Вызов метода для добавления инструмента в систему
            DataManager.instance.AddTool(newTool);  
            return true;
        }
    }
}
