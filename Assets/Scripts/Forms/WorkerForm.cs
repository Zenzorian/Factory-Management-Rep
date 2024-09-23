using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using System.Collections.Generic;

namespace FactoryManager
{
    [System.Serializable]
    public class WorkerForm : BaseAddition
    {
        private Worker _worker;
        private Dictionary<string, InputField> _inputFields;
        public WorkerForm(InputFieldCreator inputFieldCreator, Transform content, Button button) : base(inputFieldCreator, content, button)
        {
            _inputFields = BuildAdditionPanel(typeof(Worker));
            button.onClick.AddListener(Save);
        }
        public void Open(List<Worker> workers, TableItem currentWorker)
        {
            var desiredWorker = (Worker)currentWorker;
            _worker = workers[workers.IndexOf(desiredWorker)];

            _inputFields["Id"].text = _worker.Id.ToString();
            Debug.Log(_worker.Name);
            _inputFields["Name"].text = _worker.Name;
            _inputFields["Type"].text = _worker.Type;

            _inputFields["WeeklyNorm"].text = _worker.WeeklyNorm.ToString();
            _inputFields["OvertimeAllowed"].text = _worker.OvertimeAllowed.ToString();
            _inputFields["HourlyWage"].text = _worker.HourlyWage.ToString();
            _inputFields["OvertimeSurcharge"].text = _worker.OvertimeSurcharge.ToString();
            _inputFields["NightShiftSurcharge"].text = _worker.NightShiftSurcharge.ToString();           
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

            _worker.Id = id.Value;
            _worker.Name = name;
            _worker.Type = type;

            _worker.WeeklyNorm = (float)weeklyNorm.Value;
            _worker.OvertimeAllowed = (float)overtimeAllowed.Value;
            _worker.HourlyWage = (float)hourlyWage.Value;
            _worker.OvertimeSurcharge = (float)overtimeAllowed.Value;
            _worker.NightShiftSurcharge = (float)nightShiftSurcharge.Value;
        }
    }
}
