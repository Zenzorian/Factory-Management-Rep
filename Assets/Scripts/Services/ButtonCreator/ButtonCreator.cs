using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    [System.Serializable]
    public class ButtonCreator : IButtonCreator
    {
        private readonly Button _buttonPrefab;
        private readonly Button _deleteButton;       

        public ButtonCreator(Button button, Button deleteButton)
        {
            _buttonPrefab = button;  
            _deleteButton = deleteButton;
        }
        public List<Transform> Create(string[] names, Transform contentTransform)
        {
            List<Transform> buttons = new List<Transform>();

            foreach (var name in names)
            {              
                Button button = Object.Instantiate(_buttonPrefab, contentTransform);
                Text textComponent = button.GetComponentInChildren<Text>();
                textComponent.text = name;
                button.name = name;
                buttons.Add(button.transform);
            }
            
            return buttons;
        }
        public Button CreateDeleteButton(Transform parent)
        {
            var deleteButton = GameObject.Instantiate(_deleteButton);
            deleteButton.transform.SetParent(parent);
            
            var deleteButtonRect = deleteButton.GetComponent<RectTransform>();           
            deleteButtonRect.pivot = new Vector2(1, 1);
            deleteButtonRect.anchorMax = new Vector2(1, 1);
            deleteButtonRect.anchorMin = new Vector2(1, 1);
            deleteButtonRect.anchoredPosition = new Vector2(-5, -5); 
          
            deleteButton.gameObject.SetActive(false);

            return deleteButton;
    }
    }
}