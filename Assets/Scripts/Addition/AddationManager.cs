using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FactoryManager
{
    public class AddationManager : MonoBehaviour
    {
        public static AddationManager instance;
        public UnityEvent OnAdded;

        [SerializeField] private ChioceListAddation _chioceListAddation;
        [SerializeField] private Button _addButton;

        private void Awake()
        {
            if (instance == null)
                instance = this;           
        }
        public void AddChioceList() 
        {
            _chioceListAddation.Set(ChoiceOfCategoryMenu.MenuType,_addButton);
        }
        

    }
}