using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FactoryManager
{
    public class TableModel : MonoBehaviour
    {
        [SerializeField] private TableView _tableManager;
        [SerializeField] private GlobalData _globalData;       
              
        public void SetList(Type type, int value)
        {
            Debug.Log(type);
            switch (type.Name)
            {
                case "FactoryWorkspace":
                    ShowTable(Filter(GlobalData.typesOfWorkspaces[value], _globalData.listOfWorkstations));
                    break;
                case "MachineTool":                  
                    ShowTable(Filter(GlobalData.typesOfTools[value], _globalData.listOfTools));
                    break;
                case "FactoryWorker":                    
                    ShowTable(Filter(GlobalData.typesOfWorkers[value], _globalData.listOfWorkers));
                    break;
                case "PartType":
                    ShowTable(Filter(GlobalData.typesOfParts[value], _globalData.listOfParts));
                    break;
                default:
                    break;
            }
        }
        private List<T> Filter<T>(string type, List<T> list) where T : TableItem
        {
            var temporaryList = new List<T>();
            foreach (var item in list)
            {               
                if (item.Type == type)
                    temporaryList.Add(item);
            }
            return temporaryList;
        }

        private void ShowTable<T>(List<T> list)
        {
            _tableManager.ClearTable();

            if (list.Count == 0)
            {
                Debug.Log($"{list} is empty");
                return;
            }

            PropertyInfo[] properties = list[0].GetType().GetProperties();
            List<string> propertyNames = new List<string>();

            foreach (var item in properties)
            {
                propertyNames.Add(item.Name);
            }

            var tableData = new string[list.Count, propertyNames.Count];

            for (int i = 0; i < list.Count; i++)
            {
                PropertyInfo[] currentProperties = list[i].GetType().GetProperties();
                for (int j = 0; j < currentProperties.Length; j++)
                {
                    var value = currentProperties[j].GetValue(list[i]);
                    tableData[i, j] = value != null ? value.ToString() : string.Empty;
                }
            }

            Table table = new Table(propertyNames.ToArray(), tableData);
            _tableManager.CreateTable(table);
        }
    }
}