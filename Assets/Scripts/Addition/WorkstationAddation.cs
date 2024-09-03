using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace FactoryManager
{
    [System.Serializable]
    public class WorkstationAddation : BaseAddition
    {
        private Dictionary<string, InputField> _inputFields;
        private List<Tool> _selectedTools = new List<Tool>(); 
        public void Init(Dictionary<string, InputField> inputFields)
        {           
            _selectedTools.Clear();
            MenuManager.instance.OnToolSelected.AddListener(SetTools);
            _inputFields = inputFields;
            var toolInput = inputFields["Tools"];
            toolInput.interactable = false;
            ColorBlock colorBlock = toolInput.colors;
            colorBlock.disabledColor = Color.white; // Задаем желаемый цвет для состояния "Disabled"
            toolInput.colors = colorBlock;
            toolInput.text = "Select Tools";            
            var selectedListener= toolInput.transform.AddComponent<InputFieldSelectListener>();
            selectedListener.onSelectAction = OpenToolTable;                   
        }
        private void OpenToolTable()
        {
            MenuManager.instance.OpenMenu((int)MainMenuTypes.Tools);
        }
        private void SetTools(Tool tool)
        {
            _selectedTools.Add(tool);
            UIPopupMessage.instance.ShowMessage("Tool successfully added");
            MenuManager.instance.Back();
            MenuManager.instance.Back();
        }
        public async Task<bool> ValidateAndCreateWorkstation(Dictionary<string, InputField> inputFields)
        {
            string type = await ValidateStringInput(inputFields["Type"]);
            if (type == null) return false;

            Tool[] tools = _selectedTools.ToArray();
            int? maxWorkers = await ValidateIntInput(inputFields["MaxWorkers"]);
            if (!maxWorkers.HasValue) return false;

            int? reservedWorkers = await ValidateIntInput(inputFields["ReservedWorkers"]);
            if (!reservedWorkers.HasValue) return false;

            Workstation newWorkstation = new Workstation(type, tools, maxWorkers.Value, reservedWorkers.Value);
          
            DataManager.instance.AddWorkstation(newWorkstation);  

            MenuManager.instance.OnToolSelected.RemoveListener(SetTools);
            return true;
        }
    }
}
