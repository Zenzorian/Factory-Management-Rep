using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System.Collections.Generic;
using UnityEngine.Events;

namespace FactoryManager
{
    [System.Serializable]
    public class WorkspaceAddation : BaseAddition
    {
        private Dictionary<string, InputField> _inputFields;
        private List<Tool> _selectedTools = new List<Tool>();

        public WorkspaceAddation(InputFieldCreator inputFieldCreator, Transform content, UnityEvent OnAdded, Button button,string elementType) : base(inputFieldCreator, content, OnAdded, button)
        {
            _inputFields = BuildAdditionPanel(typeof(Workspace),elementType);
            button.onClick.AddListener(Addation);
            Init(_inputFields);
        }
        private void Addation()
        {
            ValidateAndCreateWorkspace(_inputFields);
        }

        public void Init(Dictionary<string, InputField> inputFields)
        {           
            _selectedTools.Clear();
            MenuManager.instance.OnToolSelected.RemoveAllListeners();
            MenuManager.instance.OnToolSelected.AddListener(SetTools);
            _inputFields = inputFields;
            var toolInput = inputFields["Tools"];
            toolInput.interactable = false;
            ColorBlock colorBlock = toolInput.colors;
            colorBlock.disabledColor = Color.white; // Задаем желаемый цвет для состояния "Disabled"
            toolInput.colors = colorBlock;
            toolInput.text = "Select Tools";            
            var selectedListener= toolInput.gameObject.AddComponent<InputFieldSelectListener>();
            selectedListener.onSelectAction = OpenToolTable;                   
        }
        private void OpenToolTable()
        {
            MenuManager.instance.OpenMenu((int)MainMenuTypes.Tools);
        }
        private void SetTools(Tool tool)
        {
            Debug.Log("Tool Event");
            _selectedTools.Add(tool);
            UIPopupMessage.instance.ShowMessage("Tool successfully added");
            MenuManager.instance.Back();
            MenuManager.instance.Back();           
        }
        public async void ValidateAndCreateWorkspace(Dictionary<string, InputField> inputFields)
        {                  
            int? id = await ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await ValidateStringInput(inputFields["Name"]);
            if (name == null) return;        

            string type = await ValidateStringInput(inputFields["Type"]);
            if (type == null) return;

            Tool[] tools = _selectedTools.ToArray();
            int? maxWorkers = await ValidateIntInput(inputFields["MaxWorkers"]);
            if (!maxWorkers.HasValue) return;

            int? reservedWorkers = await ValidateIntInput(inputFields["ReservedWorkers"]);
            if (!reservedWorkers.HasValue) return;

            Workspace newWorkspace = new Workspace(id.Value, name, type, tools, maxWorkers.Value, reservedWorkers.Value);
          
            DataManager.Instance.AddItem(MainMenuTypes.Workspaces,newWorkspace);  

            Added();
        }
    }
}
