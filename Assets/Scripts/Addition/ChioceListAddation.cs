using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{ 
    [System.Serializable]
    public class ChioceListAddation
    {
        [SerializeField] private GlobalData _globalData;
        [SerializeField] private Transform additionPanel;
        [SerializeField] private Transform _content;
        [SerializeField] private InputFieldCreator _inputFieldCreator = new InputFieldCreator();

        private List<string> _list;
        private InputField _inputField;
        private Button _button;
        public void Set(MainMenuTypes types,Button addButton)
        {
            _button = addButton;
            _button.onClick.AddListener(AddToList);

            switch (types)
            {
                case MainMenuTypes.Workspace:
                    _list = _globalData.typesOfWorkspaces;
                    BuildAddationPanel(types);
                    break;
                case MainMenuTypes.Tools:
                    _list = _globalData.typesOfTools;
                    BuildAddationPanel(types);
                    break;
                case MainMenuTypes.Workers:
                    _list = _globalData.typesOfWorkers;
                    BuildAddationPanel(types);
                    break;
                case MainMenuTypes.Parts:
                    _list = _globalData.typesOfParts;
                    BuildAddationPanel(types);
                    break;
                default:
                    break;
            }


        }
        private void AddToList()
        {
            if (_inputField == null || string.IsNullOrEmpty(_inputField.text)) return;
            Debug.Log(_inputField.text);
            _list.Add(_inputField.text);
            AddationManager.instance.OnAdded.Invoke();
            _button.onClick.RemoveListener(AddToList);
        }
        private void BuildAddationPanel(MainMenuTypes menuTypes) 
        {
            Clear();

            string title = menuTypes.ToString();

            _inputField = _inputFieldCreator.Create(title,_content);
        }       
        private void Clear()
        {
            foreach (Transform item in _content)
            {
                GameObject.Destroy(item.gameObject);
            }
        }        
    }
}