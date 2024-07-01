using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;
using System;

public class AdditionView : MonoBehaviour
{
    public Transform additionPanel;
    [SerializeField] private Transform _content;
    [SerializeField] private InputFieldCreator _inputFieldCreator = new InputFieldCreator();

    [SerializeField] private AddationModel _addationModel;

    private TableItem _currentItem; // Текущий обрабатываемый объект
    private Dictionary<string, InputField> _inputFields = new Dictionary<string, InputField>(); // Словарь для хранения полей ввода
       
    public void CreateAdditionPanel(TableItem tableItem)
    {
        Clear();

        _currentItem = tableItem;
        FieldInfo[] fields = _currentItem.GetType().GetFields();
        _inputFields = _inputFieldCreator.Create(fields, _content);
    }

    // Публичный метод для заполнения свойств объекта значениями из полей ввода
    public void ApplyInputToTableItem()
    {      
        FieldInfo[] properties = _currentItem.GetType().GetFields();

        foreach (var property in properties)
        {
            if (_inputFields.ContainsKey(property.Name))
            {                
                string inputValue = _inputFields[property.Name].text;
                property.SetValue(_currentItem, inputValue);
                Debug.Log(inputValue);
            }
        }      
        _addationModel.AddToList(_currentItem);       
    }
    public void Close()
    {
        Clear();
        additionPanel.gameObject.SetActive(false);
    }
    private void Clear()
    {
        foreach (Transform item in _content)
        {
            Destroy(item.gameObject);
        }        
    }
}
