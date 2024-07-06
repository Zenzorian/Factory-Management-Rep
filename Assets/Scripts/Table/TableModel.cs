using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    public class TableModel : MonoBehaviour
    {
        [SerializeField] private TableView _tableManager;
        [SerializeField] private GlobalData _globalData;

        private void Awake()
        {
            _globalData.listOfWorkers = WorkersGenerator.GenerateWorkers(30);
        }
        public void SetList(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Consumable:
                    break;
                case DataType.Operation:
                    break;
                case DataType.Part:
                    break;
                case DataType.Station:
                    break;
                case DataType.Tool:
                    break;
                case DataType.Worker:
                    ShowTable(_globalData.listOfWorkers);
                    break;
                default:
                    break;
            }
        }

        private void ShowTable<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                Debug.Log($"{list} is empty");
                return;
            }

            FieldInfo[] fields = list[0].GetType().GetFields();
            List<string> fieldsNames = new List<string>();

            foreach (var item in fields)
            {
                fieldsNames.Add(item.Name);
            }
            var tableData = new string[list.Count, fieldsNames.Count];
            
            for (int i = 0; i < list.Count; i++)
            {
                FieldInfo[] currentFields = list[i].GetType().GetFields();
                for (int j = 0; j < currentFields.Length; j++)
                {                    
                    tableData[i, j] = (string)currentFields[j].GetValue(list[i]);
                }
            }

            Table table = new Table(fieldsNames.ToArray(), tableData);
            _tableManager.CreateTable(table);
        }
    }
}