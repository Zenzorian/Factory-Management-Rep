using FactoryManager.Data;
using FactoryManager.Data.Tools;
using System;
using System.Collections;
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
        public void SetList(Type type, int value)
        {
            switch (type.Name)
            {
                case "FactoryWorkspace":
                   
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
        //private void WorkspaceFilter(List<Workspace> list, FactoryWorkspace factoryWorkspace)
        //{
        //    foreach (var item in list)
        //    {
        //        if()
        //        _temporaryList
        //    }
        //}

        private List<Tool> Filter(MachineTool type)
        {
            var temporaryList = new List<Tool>();
            foreach (var item in _globalData.listOfTool)
            {
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
                if (item.position == type)
                    temporaryList.Add(item);
            }
            return temporaryList;
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
                    Debug.Log(currentFields[j].GetValue(list[i]));
                    tableData[i, j] = currentFields[j].GetValue(list[i]).ToString();
                }
            }

            Table table = new Table(fieldsNames.ToArray(), tableData);
            _tableManager.CreateTable(table);
        }
    }
}