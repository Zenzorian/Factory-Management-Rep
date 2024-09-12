using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FactoryManager
{
    [System.Serializable]
    public class ChioceListAddation : BaseAddition
    {
        private List<string> _list;

        public ChioceListAddation(InputFieldCreator inputFieldCreator, Transform content, UnityEvent OnAdded, Button button) : base(inputFieldCreator, content, OnAdded, button)
        {
            
        }

        public void Open(List<string> list)
        {      
            _list = list;              
            _button.onClick.AddListener(AddToList);
            
            Clear();
            string title = "Set the name of a new category";
            _inputField = _inputFieldCreator.Create(title, _content);
        }

        public async void AddToList()
        {
            string name = await ValidateStringInput(_inputField);
            if (name == null) return;
                    
            _list.Add(_inputField.text);
            _OnAdded.Invoke();
            _button.onClick.RemoveListener(AddToList);
        }            
    }
}
