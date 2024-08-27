using System;

using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    public abstract class BaseAddition
    {
        protected GlobalData _globalData;
        protected Transform _content;
        protected InputFieldCreator _inputFieldCreator;
        protected Button _button;
        protected InputField _inputField;
        public virtual void Init(InputFieldCreator inputFieldCreator, Transform content, GlobalData globalData)
        {
            _inputFieldCreator = inputFieldCreator;
            _content = content;
            _globalData = globalData;
        }
        public abstract void Set(MainMenuTypes types, Button addButton);
       
        public virtual void BuildAdditionPanel(MainMenuTypes menuTypes)
        {
            Clear();
            string title = menuTypes.ToString();
            _inputField = _inputFieldCreator.Create(title, _content);
        }
        public virtual void BuildAdditionPanel(Type type)
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

            // Заменяем запятую на точку для унификации
            string processedText = input.Replace(',', '.');

            // Преобразуем текст в double с использованием инвариантной культуры
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

        // Асинхронный метод для обработки InputField
        public async Task<double?> ProcessInputFieldAsync(InputField inputField, bool isDecimal = false)
        {
            // Проверяем, если текст пустой
            string inputText = inputField.text;
            if (string.IsNullOrEmpty(inputText))
            {
                await FlashRed(inputField);
                return null;
            }

            // Если работает с дробями, используем ConvertTextToDouble
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
            // Проверяем, если текст пустой
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
    }    
}
