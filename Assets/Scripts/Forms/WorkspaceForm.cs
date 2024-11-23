using UnityEngine.UI;
using UnityEngine;
using Scripts.Data;
using System.Collections.Generic;

namespace Scripts
{
    [System.Serializable]
    public class WorkspaceForm : BaseAddition
    {
        private Workspace _workspace;
        private Dictionary<string, InputField> _inputFields;
        private List<Tool> _selectedTools = new List<Tool>();

        public WorkspaceForm(InputFieldCreator inputFieldCreator, Transform content, Button button) : base(inputFieldCreator, content, button)
        {
            _inputFields = BuildAdditionPanel(typeof(Workspace));
            button.onClick.AddListener(Addation);
            Init(_inputFields);
        }
        public void Open(List<Workspace> workspaces, TableItem currentWorkspace)
        {
            var desiredWorkspace = (Workspace)currentWorkspace;
            _workspace = workspaces[workspaces.IndexOf(desiredWorkspace)];

            _inputFields["Id"].text = _workspace.Id.ToString();
            _inputFields["Name"].text = _workspace.Name;
            _inputFields["Type"].text = _workspace.Type;

            _inputFields["MaxWorkers"].text = _workspace.MaxWorkers.ToString();
            _inputFields["ReservedWorkersd"].text = _workspace.ReservedWorkers.ToString();
        }
        private void Addation()
        {
            ValidateAndCreateWorkspace(_inputFields);
        }

        public void Init(Dictionary<string, InputField> inputFields)
        {           
            _selectedTools.Clear();
            MenuManager.Instance.OnToolSelected.RemoveAllListeners();
            MenuManager.Instance.OnToolSelected.AddListener(SetTools);
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
            MenuManager.Instance.OpenMenu((int)MainMenuTypes.Tools);
        }
        private void SetTools(Tool tool)
        {
            Debug.Log("Tool Event");
            _selectedTools.Add(tool);
            //PopupMessageService.instance.Show("Tool successfully added");
            MenuManager.Instance.Back();
            MenuManager.Instance.Back();           
        }
        public async void ValidateAndCreateWorkspace(Dictionary<string, InputField> inputFields)
        {                  
            int? id = await _validator.ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await _validator.ValidateStringInput(inputFields["Name"]);
            if (name == null) return;        

            string type = await _validator.ValidateStringInput(inputFields["Type"]);
            if (type == null) return;

            Tool[] tools = _selectedTools.ToArray();
            int? maxWorkers = await _validator.ValidateIntInput(inputFields["MaxWorkers"]);
            if (!maxWorkers.HasValue) return;

            int? reservedWorkers = await _validator.ValidateIntInput(inputFields["ReservedWorkers"]);
            if (!reservedWorkers.HasValue) return;
           
            _workspace.Id = id.Value;
            _workspace.Name = name;
            _workspace.Type = type;
            _workspace.MaxWorkers = maxWorkers.Value;
            _workspace.ReservedWorkers = reservedWorkers.Value;
        }
    }
}
