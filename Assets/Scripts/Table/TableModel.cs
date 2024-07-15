using FactoryManager.Data;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FactoryManager
{
    public class TableModel : MonoBehaviour
    {
        [SerializeField] private TableView _tableManager;
        [SerializeField] private GlobalData _globalData;       
              
        public void SetList(MainMenuTypes menuType, int value)
        {            
            switch (menuType)
            {
                case MainMenuTypes.Workspace:
                    ShowTable(Filter(_globalData.typesOfWorkspaces[value], _globalData.listOfWorkstations));
                    break;
                case MainMenuTypes.Tools:                  
                    ShowTable(Filter(_globalData.typesOfTools[value], _globalData.listOfTools));
                    break;
                case MainMenuTypes.Workers:                    
                    ShowTable(Filter(_globalData.typesOfWorkers[value], _globalData.listOfWorkers));
                    break;
                case MainMenuTypes.Parts:
                    ShowTable(Filter(_globalData.typesOfParts[value], _globalData.listOfParts));
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

            FieldInfo[] fields = list[0].GetType().GetFields();
            List<string> fieldNames = new List<string>();

            foreach (var item in fields)
            {
                fieldNames.Add(item.Name);
            }

            var tableData = new string[list.Count, fieldNames.Count];

            for (int i = 0; i < list.Count; i++)
            {
                FieldInfo[] currentFields = list[i].GetType().GetFields();
                for (int j = 0; j < currentFields.Length; j++)
                {
                    var value = currentFields[j].GetValue(list[i]);
                    tableData[i, j] = value != null ? value.ToString() : string.Empty;
                }
            }

            Table table = new Table(fieldNames.ToArray(), tableData);
            _tableManager.CreateTable(table);
        }
    }
}