using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;

namespace FactoryManager
{
    [System.Serializable]
    public class WorkerAddation : BaseAddition
    {
        [SerializeField] private WorkerForm _workerForm;

        public override void Set(MainMenuTypes types, Button addButton)
        {
            if (types != MainMenuTypes.Workers) return;

            _workerForm.Open(DataManager.instance.GetTypesOfWorkers());
            addButton.onClick.AddListener(ValidateAndCreateWorker);
        }

        private async void ValidateAndCreateWorker()
        {
            int? id = await ValidateIntInput(_workerForm.idInputField);
            if (!id.HasValue) return;

            string firstName = await ValidateStringInput(_workerForm.firstNameInputField);
            if (firstName == null) return;

            string lastName = await ValidateStringInput(_workerForm.lastNameInputField);
            if (lastName == null) return;

            double? weeklyNorm = await ValidateDoubleInput(_workerForm.weeklyNormInputField);
            if (!weeklyNorm.HasValue) return;

            double? overtimeAllowed = await ValidateDoubleInput(_workerForm.overtimeAllowedInputField);
            if (!overtimeAllowed.HasValue) return;

            double? hourlyWage = await ValidateDoubleInput(_workerForm.hourlyWageInputField);
            if (!hourlyWage.HasValue) return;

            double? overtimeSurcharge = await ValidateDoubleInput(_workerForm.overtimeSurchargeInputField);
            if (!overtimeSurcharge.HasValue) return;

            double? nightShiftSurcharge = await ValidateDoubleInput(_workerForm.nightShiftSurchargeInputField);
            if (!nightShiftSurcharge.HasValue) return;

            Worker newWorker = new Worker(
                id: id.Value,
                firstName: firstName,
                lastName: lastName,
                type: _workerForm.typeDropdown.options[_workerForm.typeDropdown.value].text,
                weeklyNorm: (float)weeklyNorm.Value,
                overtimeAllowed: (float)overtimeAllowed.Value,
                hourlyWage: (float)hourlyWage.Value,
                overtimeSurcharge: (float)overtimeSurcharge.Value,
                nightShiftSurcharge: (float)nightShiftSurcharge.Value
            );

            DataManager.instance.AddWorker(newWorker);
            _workerForm.Close();
        }

        
    }
}
