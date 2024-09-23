using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FactoryManager
{
    public abstract class BaseAddition
    {        
        protected Transform _content;
        protected InputFieldCreator _inputFieldCreator;
        protected Button _button;
        protected InputField _inputField;
        protected UnityEvent _OnAdded;
        protected InputFieldValidator _validator = new InputFieldValidator();
        public BaseAddition(InputFieldCreator inputFieldCreator, Transform content, Button button, UnityEvent OnAdded = null)
        {
            _inputFieldCreator = inputFieldCreator;
            _content = content;  
            _OnAdded = OnAdded;  
            _button = button;      
        }    
        public void Added()
        {            
            _OnAdded.Invoke();
            _button.onClick.RemoveAllListeners();
            if(MenuManager.Instance != null)
            {
                Debug.Log("Something is added");
                MenuManager.Instance.Back();
            }
        }
        public Dictionary<string, InputField> BuildAdditionPanel(Type type, string elementType = null)
        {
            Clear();            
            var inputFields = _inputFieldCreator.Create(type, _content,elementType);
            return inputFields;
        }
        
        public virtual void Clear()
        {
            foreach (Transform item in _content)
            {
                GameObject.Destroy(item.gameObject);
            }
        }  
    }    
}
