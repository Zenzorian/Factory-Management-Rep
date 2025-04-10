using UnityEngine.UI;
using Scripts.Data;
using System.Collections.Generic;
using Scripts.Services;
using Scripts.Infrastructure.AssetManagement;
using Scripts.UI.Markers;

namespace Scripts
{
    [System.Serializable]
    public class WorkerForm : BaseAddition
    {
        private Employee _employee;
        private Dictionary<string, InputField> _inputFields;
        public WorkerForm
        (
            ISaveloadDataService saveloadDataService,
            ItemsAddationViewElements itemsAddationViewElements, 
            GlobalUIElements globalUIElements
        ) : base(saveloadDataService, itemsAddationViewElements, globalUIElements)
        {
            _inputFields = BuildAdditionPanel(typeof(Employee));
            itemsAddationViewElements.addButton.onClick.AddListener(Save);
        }
        public void Open(List<Employee> workers, TableItem currentWorker)
        {
            var desiredWorker = (Employee)currentWorker;
            _employee = workers[workers.IndexOf(desiredWorker)];

            _inputFields["Id"].text = _employee.Id.ToString();           
            _inputFields["Name"].text = _employee.Name;
            _inputFields["Type"].text = _employee.Type;

            _inputFields["WeeklyNorm"].text = _employee.WeeklyNorm.ToString();
            _inputFields["OvertimeAllowed"].text = _employee.OvertimeAllowed.ToString();
            _inputFields["HourlyWage"].text = _employee.HourlyWage.ToString();
            _inputFields["OvertimeSurcharge"].text = _employee.OvertimeSurcharge.ToString();
            _inputFields["NightShiftSurcharge"].text = _employee.NightShiftSurcharge.ToString();           
        }
        private void Save()
        {
            ValidateAndSaveWorker(_inputFields);
        }
        public async void  ValidateAndSaveWorker(Dictionary<string, InputField> inputFields)
        {
            int? id = await _validator.ValidateIntInput(inputFields["Id"]);            
            if (!id.HasValue) return;

            string name = await _validator.ValidateStringInput(inputFields["Name"]);
            if (name == null) return;

            string type = await _validator.ValidateStringInput(inputFields["Type"]);
            if (type == null) return;   

            double? weeklyNorm = await _validator.ValidateDoubleInput(inputFields["WeeklyNorm"]);
            if (!weeklyNorm.HasValue) return;

            double? overtimeAllowed = await _validator.ValidateDoubleInput(inputFields["OvertimeAllowed"]);
            if (!overtimeAllowed.HasValue) return;

            double? hourlyWage = await _validator.ValidateDoubleInput(inputFields["HourlyWage"]);
            if (!hourlyWage.HasValue) return;

            double? overtimeSurcharge = await _validator.ValidateDoubleInput(inputFields["OvertimeSurcharge"]);
            if (!overtimeSurcharge.HasValue) return;

            double? nightShiftSurcharge = await _validator.ValidateDoubleInput(inputFields["NightShiftSurcharge"]);
            if (!nightShiftSurcharge.HasValue) return;

            _employee.Id = id.Value;
            _employee.Name = name;
            _employee.Type = type;

            _employee.WeeklyNorm = (float)weeklyNorm.Value;
            _employee.OvertimeAllowed = (float)overtimeAllowed.Value;
            _employee.HourlyWage = (float)hourlyWage.Value;
            _employee.OvertimeSurcharge = (float)overtimeAllowed.Value;
            _employee.NightShiftSurcharge = (float)nightShiftSurcharge.Value;
        }
    }
}
