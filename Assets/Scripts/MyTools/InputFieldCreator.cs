using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InputFieldCreator
{
    [SerializeField] private GameObject _inputFieldPrefab;
    [SerializeField] private GameObject _textPrefab;

    [SerializeField] private Vector2 _elementSize = new Vector2(100, 100); // Размер по умолчанию
    [SerializeField] private float _parentHeight = 100; // Высота родительского объекта по умолчанию

    public Dictionary<string, InputField> Create(Type type, Transform panelTransform, string elementType = null)
    {
        Dictionary<string, InputField> inputFields = new Dictionary<string, InputField>();
        FieldInfo[] fields = type.GetFields();
       
        foreach (var field in fields)
        {            
            InputField inputField = CreateElement(field.Name, field.FieldType, panelTransform);
            if (field.Name == "Type")
            {
                inputField.text = elementType;
                inputField.interactable = false;
            }
            inputFields.Add(field.Name, inputField);
        }

        return inputFields;
    }

    public InputField Create(string title, Transform panelTransform)
    {
        return CreateElement(title, typeof(string), panelTransform);
    }

    private InputField CreateElement(string title, Type fieldType, Transform panelTransform)
    {     
        // Создаем родительский объект
        GameObject parentGO = CreateParentObject(title, panelTransform);
        
        // Создаем текстовую метку
        CreateLabel(title, parentGO.transform);
        
        // Создаем поле ввода
        InputField inputField = CreateInputField(parentGO.transform);
        
        // Настраиваем тип содержимого поля ввода
        SetInputFieldContentType(inputField, fieldType);
        
        return inputField;
    }
    
    private GameObject CreateParentObject(string title, Transform panelTransform)
    {
        GameObject parentGO = new GameObject(title + "_Parent", typeof(RectTransform));
        parentGO.transform.SetParent(panelTransform, false);
        
        RectTransform parentRectTransform = parentGO.GetComponent<RectTransform>();
        parentRectTransform.sizeDelta = new Vector2(panelTransform.GetComponent<RectTransform>().rect.width - 40, _parentHeight);
        parentRectTransform.anchorMin = new Vector2(0, 0.5f);
        parentRectTransform.anchorMax = new Vector2(1, 0.5f);
        parentRectTransform.pivot = new Vector2(0.5f, 0.5f);
        
        return parentGO;
    }
    
    private void CreateLabel(string title, Transform parentTransform)
    {
        GameObject textGO = GameObject.Instantiate(_textPrefab, parentTransform);
        Text textComponent = textGO.GetComponent<Text>();
        textComponent.text = title;
        textComponent.alignment = TextAnchor.MiddleLeft;
        
        RectTransform textRectTransform = textGO.GetComponent<RectTransform>();
        textRectTransform.anchorMin = new Vector2(0f, 0.5f);
        textRectTransform.anchorMax = new Vector2(0.45f, 0.5f);
        textRectTransform.pivot = new Vector2(0f, 0.5f);
        textRectTransform.sizeDelta = new Vector2(0, _parentHeight);
        textRectTransform.anchoredPosition = Vector2.zero;
    }
    
    private InputField CreateInputField(Transform parentTransform)
    {
        GameObject inputFieldGO = GameObject.Instantiate(_inputFieldPrefab, parentTransform);
        InputField inputField = inputFieldGO.GetComponent<InputField>();
        
        RectTransform inputFieldRectTransform = inputFieldGO.GetComponent<RectTransform>();
        inputFieldRectTransform.anchorMin = new Vector2(0.55f, 0.5f);
        inputFieldRectTransform.anchorMax = new Vector2(1, 0.5f);
        inputFieldRectTransform.pivot = new Vector2(0.5f, 0.5f);
        inputFieldRectTransform.sizeDelta = new Vector2(0, _parentHeight);
        inputFieldRectTransform.anchoredPosition = Vector2.zero;
        
        return inputField;
    }
    
    private void SetInputFieldContentType(InputField inputField, Type fieldType)
    {
        if (fieldType == typeof(int))
        {
            inputField.contentType = InputField.ContentType.IntegerNumber;
        }
        else if (fieldType == typeof(float) || fieldType == typeof(double))
        {
            inputField.contentType = InputField.ContentType.DecimalNumber;
        }
        else
        {
            inputField.contentType = InputField.ContentType.Standard;
        }
    }
}
