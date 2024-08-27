using FactoryManager;
using FactoryManager.Data;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WorkerForm : BaseAddition
{
    [SerializeField] private InputField _IdInputField;

    [SerializeField] private Dropdown _typeDropdown;

    [SerializeField] private InputField _firstNameInputField;
    [SerializeField] private InputField _lastNameInputField;

    [SerializeField] private InputField _weeklyNormInputField;
    [SerializeField] private InputField _overtimeAllowedInputField;

    [SerializeField] private InputField _hourlyWageInputField;
    [SerializeField] private InputField _overtimeSurchargeInputField;
    [SerializeField] private InputField _nightShiftSurchargeInputField;

    public override void Set(MainMenuTypes types, Button addButton)
    {
        throw new System.NotImplementedException();
    }
    private async Task ValidateAndCreateWorker()
    {
        int? id = int.Parse(await ProcessInputFieldAsync(_IdInputField));
        string firstName = await ProcessInputFieldAsync(_firstNameInputField);
        string lastName = await ProcessInputFieldAsync(_lastNameInputField);

        double? weeklyNorm = await ProcessInputFieldAsync(_weeklyNormInputField, true);
        double? overtimeAllowed = await ProcessInputFieldAsync(_overtimeAllowedInputField, true);
        double? hourlyWage = await ProcessInputFieldAsync(_hourlyWageInputField, true);
        double? overtimeSurcharge = await ProcessInputFieldAsync(_overtimeSurchargeInputField, true);
        double? nightShiftSurcharge = await ProcessInputFieldAsync(_nightShiftSurchargeInputField, true);


        // Проверяем, что все поля успешно прошли валидацию
        if (id.HasValue && firstName!=null && lastName != null && weeklyNorm.HasValue && overtimeAllowed.HasValue && hourlyWage.HasValue &&
            overtimeSurcharge.HasValue && nightShiftSurcharge.HasValue)
        {
            // Создаем экземпляр класса Worker
            Worker newWorker = new Worker(
                id: (int)id, // Используйте подходящий механизм для генерации уникального ID
                firstName: firstName,
                lastName: lastName,
                type: "Type", // Определите, как вы хотите задавать тип
                weeklyNorm: (float)weeklyNorm.Value,
                overtimeAllowed: (float)overtimeAllowed.Value,
                hourlyWage: (float)hourlyWage.Value,
                overtimeSurcharge: (float)overtimeSurcharge.Value,
                nightShiftSurcharge: (float)nightShiftSurcharge.Value
            );

            // Логика для сохранения или использования нового работника
            Debug.Log("Worker created: " + newWorker.FirstName + " " + newWorker.LastName);
        }
        else
        {
            Debug.LogError("Failed to create Worker due to invalid input.");
        }
    }
}
