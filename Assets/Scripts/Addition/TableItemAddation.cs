using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    [System.Serializable]
    public class TableItemAddation : BaseAddition
    {       
        private Dictionary<string, InputField> _inputFields = new Dictionary<string, InputField>();
        
        private WorkerAddation _workerAddation = new WorkerAddation();
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
        private void AddWorkstation()
        {
            var newWorkstation = new Workstation
            (
                type: _inputFields["Type"].text,
                tools: new Tool[] { },
                maxWorkers: int.Parse(_inputFields["MaxWorkers"].text),
                reservedWorkers: int.Parse(_inputFields["ReservedWorkers"].text)
            );                       
            Added();
        }
        private async void AddWorker()
        {
            var sucsess = await _workerAddation.ValidateAndCreateWorker(_inputFields);

            if (sucsess == false) return;

            Added();

        }
        private void AddPart()
        {
            var newPart = new Part
            (
                name: _inputFields["Name"].text,
                partType: _inputFields["PartType"].text                
            );           
           
            Added();
        }
        private void AddTool()
        {
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
