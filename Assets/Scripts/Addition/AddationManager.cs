using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FactoryManager
{
    public class AddationManager : MonoBehaviour
    {
        public static AddationManager instance;
        public UnityEvent OnAdded;

        [SerializeField] private GlobalData _globalData;
        [SerializeField] private Transform _content;
        [SerializeField] private InputFieldCreator _inputFieldCreator = new InputFieldCreator();
        [SerializeField] private ChioceListAddation _chioceListAddation;
        [SerializeField] private TableItemAddation _tableItemAddation;
        [SerializeField] private Button _addButton;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            _chioceListAddation.Init(_inputFieldCreator, _content, _globalData);
            _tableItemAddation.Init(_inputFieldCreator, _content, _globalData);
        }

        public void AddChioceList()
        {            
            _chioceListAddation.Set(ChoiceOfCategoryMenu.MenuType, _addButton);
        }

        public void AddTableItem()
        {
            _tableItemAddation.Set(ChoiceOfCategoryMenu.MenuType, _addButton);
        }
    }
}
