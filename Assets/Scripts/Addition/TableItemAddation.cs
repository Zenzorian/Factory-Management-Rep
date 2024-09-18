using System.Collections;
using System.Collections.Generic;
using FactoryManager;
using FactoryManager.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TableItemAddation : BaseAddition
{    
    private MainMenuTypes _menuType;
    private Dictionary<string, InputField> _inputFields;
   
    public TableItemAddation(InputFieldCreator inputFieldCreator, Transform content, UnityEvent OnAdded, Button button) : base(inputFieldCreator, content, OnAdded, button)
    {        
          
    }
    public void Open(MainMenuTypes menuType, string elementType)
    {           
        _menuType = menuType;
        _button.onClick.AddListener(Addation);          
        _inputFields = BuildAdditionPanel(typeof(TableItem),elementType);        
    }
    private void Addation()
    {
        ValidateAndCreate(_inputFields);
        Debug.Log("asdasdasd");
    }
    public async void  ValidateAndCreate(Dictionary<string, InputField> inputFields)
    {
        int? id = await ValidateIntInput(inputFields["Id"]);
        if (!id.HasValue) return;

        string name = await ValidateStringInput(inputFields["Name"]);
        if (name == null) return;            

        TableItem newItem = new TableItem(
            id: id.Value,
            name: name,                
            type: inputFields["Type"].text                
        );
        if(DataManager.Instance != null)DataManager.Instance.AddItem(_menuType,newItem);
        Added();
    }
    }

