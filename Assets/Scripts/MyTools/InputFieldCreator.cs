using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InputFieldCreator
{
    [SerializeField] private GameObject _inputFieldPrefab; // Префаб поля ввода
    [SerializeField] private GameObject _textPrefab; // Префаб текста

    [SerializeField] private Vector2 _elementSize;
    public Dictionary<string, InputField> Create(Type type, Transform panelTransform)
    {
        Dictionary<string, InputField> inputFields = new Dictionary<string, InputField>();
        FieldInfo[] fields = type.GetFields();

        foreach (var field in fields)
        {
            InputField inputField = CreateElement(field.Name, field.FieldType, panelTransform);
            inputFields.Add(field.Name, inputField);
        }

        return inputFields;
    }

    public InputField Create(string title, Transform panelTransform)
    {
        return CreateElement($"Add type of {title}", typeof(string), panelTransform);
    }

    private InputField CreateElement(string title, Type fieldType, Transform panelTransform)
    {
        GameObject textGO = GameObject.Instantiate(_textPrefab, panelTransform);
        Text textComponent = textGO.GetComponent<Text>();
        textComponent.text = title;

        RectTransform textRectTransform = textGO.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = _elementSize;

        GameObject inputFieldGO = GameObject.Instantiate(_inputFieldPrefab, panelTransform);
        InputField inputField = inputFieldGO.GetComponent<InputField>();

        RectTransform inputFieldRectTransform = inputFieldGO.GetComponent<RectTransform>();
        inputFieldRectTransform.sizeDelta = _elementSize;

        // Настройка типа контента InputField в зависимости от типа поля
        if (fieldType == typeof(int))
        {
            inputField.contentType = InputField.ContentType.IntegerNumber;
        }
        else if (fieldType == typeof(float) || fieldType == typeof(double))
        {
            inputField.contentType = InputField.ContentType.DecimalNumber;
        }
        else if (fieldType == typeof(string))
        {
            inputField.contentType = InputField.ContentType.Standard;
        }
        else
        {
            inputField.contentType = InputField.ContentType.Standard;
        }

        return inputField;
    }
}

