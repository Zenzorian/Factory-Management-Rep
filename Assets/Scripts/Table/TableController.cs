using System;
using UnityEngine;

namespace FactoryManager
{ 
    public class TableController : MonoBehaviour
    {
        [SerializeField] private TableModel _tableModel;
        //public void OpenTable(int value)
        //{
        //    _tableModel.SetList((DataType)value);
        //}
        public void OpenTableWithFilter(Type type, int value)
        {
            _tableModel.SetList(type,value);
        }
    }
}