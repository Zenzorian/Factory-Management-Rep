using FactoryManager.Data;
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
        private MainMenuTypes _temporaryMenuType;
        private int _temporaryValue;

        private void Awake()
        {            
            AddationManager.instance.OnAdded.AddListener(SomethingAdded);
        }       
        private void SomethingAdded() 
        {
           SetList(_temporaryMenuType, _temporaryValue);
        }
        public void SetList(MainMenuTypes menuType, int value)
        {   
            _temporaryMenuType = menuType;
            _temporaryValue = value;         
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
                case MainMenuTypes.StatisticPart:
                    ShowTable(Filter(_globalData.typesOfParts[value], _globalData.listOfParts));
                    break;
                case MainMenuTypes.StatisticTool:
                    ShowTable(Filter(_globalData.typesOfTools[value], _globalData.listOfTools));
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
        public void CellSelected(int index,Type type)
        {
            //if (type == typeof(Part))
            //{ 
            //    var cellData = _globalData.lis
            //}
        
        }

        private void ShowTable<T>(List<T> list) where T : TableItem
        {
            _tableManager.ClearTable();

            if (list.Count == 0)
            {
                Debug.Log($"{list} is empty");
                return;
            }            
            _tableManager.SetTableData(list);
        }
    }
}