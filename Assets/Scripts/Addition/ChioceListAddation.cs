using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts
{
    [System.Serializable]
    public class ChioceListAddation : BaseAddition
    {
        private List<string> _list;

        public ChioceListAddation(InputFieldCreator inputFieldCreator, Transform content, Button button, UnityEvent OnAdded) : base(inputFieldCreator, content,button, OnAdded)
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
            string name = await _validator.ValidateStringInput(_inputField);
            if (name == null) return;
                    
            _list.Add(_inputField.text);        
            _button.onClick.RemoveListener(AddToList);
            Added();
        }            
    }
}
