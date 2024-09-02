using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    [System.Serializable]
    public class ChioceListAddation : BaseAddition
    {
        private List<string> _list;

        public void Set(MainMenuTypes types, Button addButton)
        {            
            _button = addButton;
            _button.onClick.AddListener(AddToList);

            switch (types)
            {
                case MainMenuTypes.Workspace:
                    _list = DataManager.instance.GetTypesOfWorkspaces();
                    break;
                case MainMenuTypes.Tools:
                    _list = DataManager.instance.GetTypesOfTools();
                    break;
                case MainMenuTypes.Workers:
                    _list = DataManager.instance.GetTypesOfWorkers();
                    break;
                case MainMenuTypes.Parts:
                    _list = DataManager.instance.GetTypesOfParts();
                    break;
                default:
                    break;
            }

            BuildAdditionPanel(types);
        }

        public void AddToList()
        {
            if (_inputField == null || string.IsNullOrEmpty(_inputField.text)) return;
            Debug.Log(_inputField.text);
            _list.Add(_inputField.text);
            AddationManager.instance.OnAdded.Invoke();
            _button.onClick.RemoveListener(AddToList);
        }            
    }
}
