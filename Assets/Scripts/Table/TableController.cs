using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace FactoryManager
{ 
    public class TableController : MonoBehaviour
    {
        [SerializeField] private TableModel _tableModel;
        public void OpenTable(int value)
        {
            _tableModel.SetList((DataType)value);
        }
    }
}