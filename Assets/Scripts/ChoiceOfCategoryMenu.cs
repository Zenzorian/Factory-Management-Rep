using System;
using UnityEngine;
using UnityEngine.UI;
namespace FactoryManager
{
    public class ChoiceOfCategoryMenu : MonoBehaviour
    {
        [SerializeField] private TableController _tableController;

        [SerializeField] private ButtonCreator _buttonCreator;

        [SerializeField] private Transform _content;

        private Type _type;
        public void Create(Type type)
        {
            _type = type;
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
            _tableController.OpenTableWithFilter(_type, index);
        }

    }
}