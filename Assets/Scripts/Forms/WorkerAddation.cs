using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FactoryManager
{
    [System.Serializable]
    public class WorkerAddation : BaseAddition
    {        
        private Dictionary<string, InputField> _inputFields;
        public WorkerAddation(InputFieldCreator inputFieldCreator, Transform content, UnityEvent OnAdded, Button button,string elementType) : base(inputFieldCreator, content, OnAdded, button)
        {
            _inputFields = BuildAdditionPanel(typeof(Worker),elementType);
            button.onClick.AddListener(Addation);
        }
        private void Addation()
        {
            ValidateAndCreateWorker(_inputFields);
        }
        public async void  ValidateAndCreateWorker(Dictionary<string, InputField> inputFields)
        {
            int? id = await ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await ValidateStringInput(inputFields["Name"]);
            if (name == null) return;            

            double? weeklyNorm = await ValidateDoubleInput(inputFields["WeeklyNorm"]);
            if (!weeklyNorm.HasValue) return;

            double? overtimeAllowed = await ValidateDoubleInput(inputFields["OvertimeAllowed"]);
            if (!overtimeAllowed.HasValue) return;

            double? hourlyWage = await ValidateDoubleInput(inputFields["HourlyWage"]);
            if (!hourlyWage.HasValue) return;

            double? overtimeSurcharge = await ValidateDoubleInput(inputFields["OvertimeSurcharge"]);
            if (!overtimeSurcharge.HasValue) return;

            double? nightShiftSurcharge = await ValidateDoubleInput(inputFields["NightShiftSurcharge"]);
            if (!nightShiftSurcharge.HasValue) return;

            Worker newWorker = new Worker(
                id: id.Value,
                name: name,                
                type: inputFields["Type"].text,
                weeklyNorm: (float)weeklyNorm.Value,
                overtimeAllowed: (float)overtimeAllowed.Value,
                hourlyWage: (float)hourlyWage.Value,
                overtimeSurcharge: (float)overtimeSurcharge.Value,
                nightShiftSurcharge: (float)nightShiftSurcharge.Value
            );

            DataManager.Instance.AddItem(MainMenuTypes.Workers,newWorker);  
            Added();
        }


    }
}
