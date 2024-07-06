using System;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceOfCategoryMenu : MonoBehaviour
{
    [SerializeField] private ButtonCreator _buttonCreator;

    [SerializeField] private Transform _content;
    public void Create(Type type)
    {
        string[] names = Enum.GetNames(type);

        var buttons = _buttonCreator.Create(names, _content);

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            var myButton = buttons[index].GetComponent<Button>();
            myButton.onClick.AddListener(delegate { ButtonPressed(index); });
        }
    }

    public void ButtonPressed(int index)
    {
        Debug.Log(index);
    }

}


