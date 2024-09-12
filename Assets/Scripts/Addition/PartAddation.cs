using UnityEngine.UI;
using UnityEngine;
using FactoryManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FactoryManager
{
    [System.Serializable]
    public class PartAddation : BaseAddition
    {
        private Dictionary<string, InputField> _inputFields;
        public PartAddation(InputFieldCreator inputFieldCreator, Transform content, UnityEvent OnAdded, Button button,string elementType) : base(inputFieldCreator, content, OnAdded, button)
        {
            _inputFields = BuildAdditionPanel(typeof(Part),elementType);
            button.onClick.AddListener(Addation);
            Init(_inputFields);
        }
        private void Addation()
        {
            ValidateAndCreatePart(_inputFields);
        }

        public void Init(Dictionary<string, InputField> inputFields)
        {              
            _inputFields = inputFields;
            var toolInput = inputFields["Statistic"];
            toolInput.interactable = false;
            ColorBlock colorBlock = toolInput.colors;
            colorBlock.disabledColor = Color.white; // Задаем желаемый цвет для состояния "Disabled"
            toolInput.colors = colorBlock;
            toolInput.text = "Statistic";            
            var selectedListener= toolInput.gameObject.AddComponent<InputFieldSelectListener>();
            selectedListener.onSelectAction = StatisticsClicked;                   
        }
        private void StatisticsClicked()
        {
            UIPopupMessage.instance.ShowMessage("Statistical data is set in the Statistics tab");
        }
        public async void ValidateAndCreatePart(Dictionary<string, InputField> inputFields)
        {
            int? id = await ValidateIntInput(inputFields["Id"]);
            if (!id.HasValue) return;

            string name = await ValidateStringInput(inputFields["Name"]);
            if (name == null) return;

            string partType = await ValidateStringInput(inputFields["Type"]);
            if (partType == null) return;

            // Логика для создания объекта Part
            Part newPart = new Part( 
                id: id.Value,
                name: name,
                type: partType
            );
            // Вызов метода для добавления детали в систему
            DataManager.instance.AddPart(newPart);  

            Added();
        }
    }
}
