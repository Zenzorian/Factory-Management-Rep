using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    [System.Serializable]
    public class ChioceListAddation : BaseAddition
    {
        private List<string> _list;

        public override void Set(MainMenuTypes types, Button addButton)
        {            
            _button = addButton;
            _button.onClick.AddListener(AddToList);

            switch (types)
            {
                case MainMenuTypes.Workspace:
                    _list = _globalData.typesOfWorkspaces;
                    break;
                case MainMenuTypes.Tools:
                    _list = _globalData.typesOfTools;
                    break;
                case MainMenuTypes.Workers:
                    _list = _globalData.typesOfWorkers;
                    break;
                case MainMenuTypes.Parts:
                    _list = _globalData.typesOfParts;
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
