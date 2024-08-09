using System;
using UnityEngine;

namespace FactoryManager
{ 
    public class TableController : MonoBehaviour
    {
        [SerializeField] private TableModel _tableModel;
        [SerializeField] private Transform _addButton;
        //public void OpenTable(int value)
        //{
        //    _tableModel.SetList((DataType)value);
        //}
        public void OpenTableWithFilter(MainMenuTypes menuType, int value)
        {
            _tableModel.SetList(menuType,value);

            if (menuType == MainMenuTypes.StatisticTool ||
                menuType == MainMenuTypes.StatisticPart)
                _addButton.gameObject.SetActive(false);
            else _addButton.gameObject.SetActive(true);
        }
    }
}