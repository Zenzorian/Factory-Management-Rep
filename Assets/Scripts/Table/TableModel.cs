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
            switch (type.Name)
            {
                case "FactoryWorkspace":
                    ShowTable(Filter((FactoryWorkspace)value));
                    break;
                case "MachineTool":                  
                    ShowTable(Filter((MachineTool)value));
                    break;
                case "FactoryWorker":                    
                    ShowTable(Filter((FactoryWorker)value));
                    break;
                case "Parts":

                    break;
                default:
                    break;
            }
        }
        private List<Workstation> Filter(FactoryWorkspace type)
        {
            var temporaryList = new List<Workstation>();
            foreach (var item in _globalData.listOfWorkstation)
            {               
                if (item.WorkspaceType == type)
                    temporaryList.Add(item);
            }
            return temporaryList;
        }

        private List<Tool> Filter(MachineTool type)
        {
            var temporaryList = new List<Tool>();
            foreach (var item in _globalData.listOfTool)
            {
                Debug.Log(item.ToolType);
                if (item.ToolType == type)
                    temporaryList.Add(item);
            }
            return temporaryList;                      
        }
        private List<Worker> Filter(FactoryWorker type)
        {
            var temporaryList = new List<Worker>();
            foreach (var item in _globalData.listOfWorkers)
            {
                if (item.Position == type)
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