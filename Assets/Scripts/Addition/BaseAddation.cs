using System;
using System.Collections.Generic;
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
    }    
}
