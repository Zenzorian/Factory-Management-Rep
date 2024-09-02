using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactoryManager
{
    [System.Serializable]
    public class WorkerAddation : BaseAddition
    {       
        public async Task<bool> ValidateAndCreateWorker(Dictionary<string, InputField> inputFields)
        {
            int? id = await ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return false;

            string firstName = await ValidateStringInput(inputFields["FirstName"]);
            if (firstName == null) return false;

            string lastName = await ValidateStringInput(inputFields["LastName"]);
            if (lastName == null) return false;

            double? weeklyNorm = await ValidateDoubleInput(inputFields["WeeklyNorm"]);
            if (!weeklyNorm.HasValue) return false;

            double? overtimeAllowed = await ValidateDoubleInput(inputFields["OvertimeAllowed"]);
            if (!overtimeAllowed.HasValue) return false;

            double? hourlyWage = await ValidateDoubleInput(inputFields["HourlyWage"]);
            if (!hourlyWage.HasValue) return false;

            double? overtimeSurcharge = await ValidateDoubleInput(inputFields["OvertimeSurcharge"]);
            if (!overtimeSurcharge.HasValue) return false;

            double? nightShiftSurcharge = await ValidateDoubleInput(inputFields["NightShiftSurcharge"]);
            if (!nightShiftSurcharge.HasValue) return false;

            Worker newWorker = new Worker(
                id: id.Value,
                firstName: firstName,
                lastName: lastName,
                type: ""/*_workerForm.typeDropdown.options[_workerForm.typeDropdown.value].text*/,
                weeklyNorm: (float)weeklyNorm.Value,
                overtimeAllowed: (float)overtimeAllowed.Value,
                hourlyWage: (float)hourlyWage.Value,
                overtimeSurcharge: (float)overtimeSurcharge.Value,
                nightShiftSurcharge: (float)nightShiftSurcharge.Value
            );

            DataManager.instance.AddWorker(newWorker);  
            return true;
        }


    }
}
