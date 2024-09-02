using System;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    public abstract class BaseAddition
    {        
        protected Transform _content;
        protected InputFieldCreator _inputFieldCreator;
        protected Button _button;
        protected InputField _inputField;
        public virtual void Init(InputFieldCreator inputFieldCreator, Transform content)
        {
            _inputFieldCreator = inputFieldCreator;
            _content = content;           
        }    
        public virtual void BuildAdditionPanel(MainMenuTypes menuTypes)
        {
            Clear();
            string title = menuTypes.ToString();
            _inputField = _inputFieldCreator.Create(title, _content);
        }
        public virtual void BuildAdditionPanel(Type type, string elementType = null)
        {
            Clear();
            string title = type.ToString();
            _inputField = _inputFieldCreator.Create(title, _content);
        }
        public virtual void Clear()
        {
            foreach (Transform item in _content)
            {
                GameObject.Destroy(item.gameObject);
            }
        }
        public double? ConvertTextToDouble(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
          
            string processedText = input.Replace(',', '.');
          
            if (double.TryParse(processedText, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            else
            {
                Debug.LogError("Invalid number format: " + input);
                return null;
            }
        }
       
        public async Task<double?> ProcessInputFieldAsync(InputField inputField, bool isDecimal = false)
        {
            string inputText = inputField.text;
            if (string.IsNullOrEmpty(inputText))
            {                
                await FlashRed(inputField);
                return null;
            }
            if (isDecimal)
            {
                double? result = ConvertTextToDouble(inputText);
                if (!result.HasValue)
                {
                    await FlashRed(inputField);
                    return null;
                }
                return result.Value;
            }
            else
            {               
                if (double.TryParse(inputText, out double result))
                {
                    return result;
                }
                else
                {
                    await FlashRed(inputField);
                    return null;
                }
            }
        }
        public async Task<string> ProcessInputFieldAsync(InputField inputField)
        {
            string inputText = inputField.text;
            if (string.IsNullOrEmpty(inputText))
            {
                await FlashRed(inputField);                
                return null;
            }
            else return inputText;
        }
        private async Task FlashRed(InputField inputField)
        {
            Color originalColor = inputField.image.color;
            inputField.image.color = Color.red;
            await Task.Delay(500);
            inputField.image.color = originalColor;
        }
        public async Task<int?> ValidateIntInput(InputField inputField)
        {
            string input = await ProcessInputFieldAsync(inputField);
            if (input == null || !int.TryParse(input, out int result))
            {
                return null;
            }
            return result;
        }

        public async Task<string> ValidateStringInput(InputField inputField)
        {
            string input = await ProcessInputFieldAsync(inputField);
            if (string.IsNullOrEmpty(input))
            {                
                return null;
            }
            return input;
        }

        public async Task<double?> ValidateDoubleInput(InputField inputField)
        {
            double? input = await ProcessInputFieldAsync(inputField, true);
            if (!input.HasValue)
            {                
                return null;
            }
            return input;
        }
    }    
}
