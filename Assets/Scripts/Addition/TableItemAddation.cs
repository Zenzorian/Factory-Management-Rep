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
        
        public override void Set(MainMenuTypes types, Button addButton)
        {              
            _button = addButton;
            _button.onClick.RemoveAllListeners();

            switch (types)
            {
                case MainMenuTypes.Workspace:
                    BuildAdditionPanel(typeof(Workstation));
                    _button.onClick.AddListener(AddWorkstation);
                    break;
                case MainMenuTypes.Tools:
                    BuildAdditionPanel(typeof(Tool));
                    _button.onClick.AddListener(AddTool);
                    break;
                case MainMenuTypes.Workers:
                    BuildAdditionPanel(typeof(Worker));
                    _button.onClick.AddListener(AddWorker);
                    break;
                case MainMenuTypes.Parts:
                    BuildAdditionPanel(typeof(Part));
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
            _globalData.listOfWorkstations.Add(newWorkstation);
            Added();
        }
        private void AddWorker()
        {
            var newWorker = new Worker
            (
                id: int.Parse(_inputFields["Id"].text),
                firstName: _inputFields["FirstName"].text,
                lastName: _inputFields["LastName"].text,
                type: _inputFields["Type"].text,
                weeklyNorm: float.Parse(_inputFields["WeeklyNorm"].text),
                overtimeAllowed: float.Parse(_inputFields["OvertimeAllowed"].text),
                hourlyWage: float.Parse(_inputFields["HourlyWage"].text),
                overtimeSurcharge: float.Parse(_inputFields["OvertimeSurcharge"].text),
                nightShiftSurcharge: float.Parse(_inputFields["NightShiftSurcharge"].text)
            );
            _globalData.listOfWorkers.Add(newWorker);

            Added();

        }
        private void AddPart()
        {
            var newPart = new Part
            (
                name: _inputFields["Name"].text,
                partType: _inputFields["PartType"].text                
            );           
            _globalData.listOfParts.Add(newPart);
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

        public override void BuildAdditionPanel(Type type)
        {
            Clear();            
            _inputFields = _inputFieldCreator.Create(type, _content);
        }        
    }
}
