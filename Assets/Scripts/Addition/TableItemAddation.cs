using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    [System.Serializable]
    public class TableItemAddation : BaseAddition
    {       
        private Dictionary<string, InputField> _inputFields = new Dictionary<string, InputField>();
        
        private WorkerAddation _workerAddation = new WorkerAddation();
        private ToolAddation _toolAddation = new ToolAddation();
        private PartAddation _partAddation = new PartAddation();
        private WorkstationAddation _workstationAddation = new WorkstationAddation();
        public void Set(MainMenuTypes types, Button addButton,int value)
        {  
            _button = addButton;
            _button.onClick.RemoveAllListeners();

            string elementType;

            switch (types)
            {
                case MainMenuTypes.Workspace:
                    elementType = DataManager.instance.GetTypesOfWorkspaces()[value];
                    BuildAdditionPanel(typeof(Workstation),elementType);
                    _button.onClick.AddListener(AddWorkstation);     
                    _workstationAddation.Init(_inputFields);              
                    break;
                case MainMenuTypes.Tools:
                    elementType = DataManager.instance.GetTypesOfTools()[value];
                    BuildAdditionPanel(typeof(Tool), elementType);
                    _button.onClick.AddListener(AddTool);                    
                    break;
                case MainMenuTypes.Workers:
                    elementType = DataManager.instance.GetTypesOfWorkers()[value];
                    BuildAdditionPanel(typeof(Worker), elementType);
                    _button.onClick.AddListener(AddWorker);                    
                    break;
                case MainMenuTypes.Parts:
                    elementType = DataManager.instance.GetTypesOfParts()[value];
                    BuildAdditionPanel(typeof(Part), elementType);
                    _button.onClick.AddListener(AddPart);                    
                    break;
                default:
                    break;
            }           
        }
        private async void AddWorker()
        {
            var sucsess = await _workerAddation.ValidateAndCreateWorker(_inputFields);

            if (sucsess == false) return;

            Added();

        }
        private async void AddPart()
        {
            var sucsess = await _partAddation.ValidateAndCreatePart(_inputFields);

            if (sucsess == false) return;    
           
            Added();
        }
        private async void AddTool()
        {
            var sucsess = await _toolAddation.ValidateAndCreateTool(_inputFields);

            if (sucsess == false) return;

             Added();
        }

        private async void AddWorkstation()
        {
            var sucsess = await _workstationAddation.ValidateAndCreateWorkstation(_inputFields);

            if (sucsess == false) return;

             Added();
        }
        public void Added()
        {            
            AddationManager.instance.OnAdded.Invoke();
            _button.onClick.RemoveAllListeners();
        }

        public override void BuildAdditionPanel(Type type,string elementType)
        {            
            Clear();            
            _inputFields = _inputFieldCreator.Create(type, _content,elementType);
        }        
    }
}
