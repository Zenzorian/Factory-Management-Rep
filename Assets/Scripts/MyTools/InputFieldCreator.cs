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
    public Dictionary<string, InputField> Create(FieldInfo[] properties, Transform panelTransform)
    {
        Dictionary<string, InputField> inputFields = new Dictionary<string, InputField>();

        foreach (var property in properties)
        {
            InputField inputField = CreateElement(property.Name, panelTransform);
            inputFields.Add(property.Name, inputField);
        }

        return inputFields;
    }

    public InputField Create(string title, Transform panelTransform)
    {
        return CreateElement($"Add type of {title}", panelTransform);
    }

    private InputField CreateElement(string title, Transform panelTransform)
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

        return inputField;
    }
}

