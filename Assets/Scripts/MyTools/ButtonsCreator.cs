using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ButtonCreator
{
    [SerializeField] private GameObject _buttonPrefab; // Префаб поля ввода


    public List<Transform> Create(string[] names, Transform contentTransform)
    {
        List<Transform> buttons = new List<Transform>();

        foreach (var name in names)
        {
            // Создаем текст с названием свойства
            GameObject button = GameObject.Instantiate(_buttonPrefab, contentTransform);
            Text textComponent = button.GetComponentInChildren<Text>();
            textComponent.text = name;
            button.name = name;
            buttons.Add(button.transform);
        }
        return buttons;
    }
}