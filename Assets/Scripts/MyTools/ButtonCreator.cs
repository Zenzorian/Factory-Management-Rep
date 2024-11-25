using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MyTools
{
    [System.Serializable]
    public class ButtonCreator
    {
        private readonly GameObject _buttonPrefab;

        public ButtonCreator(GameObject buton)
        {
            _buttonPrefab = buton;  
        }
        public List<Transform> Create(string[] names, Transform contentTransform)
        {
            List<Transform> buttons = new List<Transform>();

            foreach (var name in names)
            {
                // Создаем текст с названием свойства
                GameObject button = Object.Instantiate(_buttonPrefab, contentTransform);
                Text textComponent = button.GetComponentInChildren<Text>();
                textComponent.text = name;
                button.name = name;
                buttons.Add(button.transform);
            }
            return buttons;
        }
    }
}