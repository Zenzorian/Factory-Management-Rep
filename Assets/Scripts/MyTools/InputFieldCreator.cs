using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InputFieldCreator
{
    [SerializeField] private GameObject inputFieldPrefab; // ������ ���� �����
    [SerializeField] private GameObject textPrefab; // ������ ������

    public Dictionary<string, InputField> Create(FieldInfo[] properties, Transform panelTransform)
    {
        Dictionary<string, InputField> inputFields = new Dictionary<string, InputField>();

        foreach (var property in properties)
        {
            // ������� ����� � ��������� ��������
            GameObject textGO = GameObject.Instantiate(textPrefab, panelTransform);
            Text textComponent = textGO.GetComponent<Text>();
            textComponent.text = property.Name;

            // ������� ���� ����� ��� ��������
            GameObject inputFieldGO = GameObject.Instantiate(inputFieldPrefab, panelTransform);
            InputField inputField = inputFieldGO.GetComponent<InputField>();
            inputFields.Add(property.Name, inputField);
        }
        return inputFields;
    }
}
