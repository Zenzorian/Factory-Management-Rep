using System.Collections.Generic;
using Scripts;
using Scripts.Data;
using Scripts.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TableItemAddation : BaseAddition
{    
    private MainMenuTypes _menuType;
    private Dictionary<string, InputField> _inputFields;
   
    public TableItemAddation(InputFieldCreator inputFieldCreator, Transform content, Button button, UnityEvent OnAdded) : base(inputFieldCreator, content, button, OnAdded)
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
        int? id = await _validator.ValidateIntInput(inputFields["Id"]);
        if (!id.HasValue) return;

        string name = await _validator.ValidateStringInput(inputFields["Name"]);
        if (name == null) return;            

        TableItem newItem = new TableItem(
            id: id.Value,
            name: name,                
            type: inputFields["Type"].text                
        );
        //if(ISaveloadDataService.Instance != null)DataManager.Instance.AddItem(_menuType,newItem);
        Added();
    }
    }

