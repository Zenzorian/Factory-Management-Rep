using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactoryManager
{
    [System.Serializable]
    public class PartAddation : BaseAddition
    {
        public async Task<bool> ValidateAndCreatePart(Dictionary<string, InputField> inputFields)
        {
            string name = await ValidateStringInput(inputFields["Name"]);
            if (name == null) return false;

            string partType = await ValidateStringInput(inputFields["PartType"]);
            if (partType == null) return false;

            // Логика для создания объекта Part
            Part newPart = new Part( 
                name: name,
                partType: partType
            );
            // Вызов метода для добавления детали в систему
            DataManager.instance.AddPart(newPart);  

            return true;
        }
    }
}
