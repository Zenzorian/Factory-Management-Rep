using FactoryManager;
using FactoryManager.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{    
    [SerializeField] private Transform _content;
    [SerializeField] private InputFieldCreator _inputFieldCreator = new InputFieldCreator();
    [SerializeField] private Button _saveButton;
    [SerializeField] private GlobalData _globalData;      
    
    private void Awake()
    {
        MenuManager.Instance.OnTableItemEdit.AddListener(Edit);
    }
    private void Edit(TableItem item,MainMenuTypes menuType)
    {
        
        switch (menuType)
        {
            case MainMenuTypes.Workspaces:
                var workspaceForm = new WorkspaceForm(_inputFieldCreator, _content, _saveButton);
                workspaceForm.Open(_globalData.listOfWorkspaces,item);               
                break;

            case MainMenuTypes.Tools:
                var toolForm = new ToolForm(_inputFieldCreator, _content, _saveButton);
                toolForm.Open(_globalData.listOfTools, item);
                break;

            case MainMenuTypes.Workers:
                var workerForm = new WorkerForm(_inputFieldCreator, _content, _saveButton);
                workerForm.Open(_globalData.listOfWorkers,item);
                break;

            case MainMenuTypes.Parts:
                var partForm = new PartForm(_inputFieldCreator, _content, _saveButton);
                partForm.Open(_globalData.listOfParts,item);
                break;
            default:
                Debug.LogWarning("Unsupported MainMenuType");
                break;
        }

    }
}
