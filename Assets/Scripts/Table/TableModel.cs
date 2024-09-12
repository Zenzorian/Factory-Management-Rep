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
            List<TableItem> temporaryList = new List<TableItem>();   
            switch (menuType)
            {
                case MainMenuTypes.Workstations:
                    temporaryList = Filter(_globalData.typesOfWorkstation[value], _globalData.listOfWorkstations);
                     MenuManager.instance.TemporaryTableItemType = _globalData.typesOfWorkstation[value];
                    break;
                case MainMenuTypes.Tools:                  
                    temporaryList = Filter(_globalData.typesOfTools[value], _globalData.listOfTools);
                     MenuManager.instance.TemporaryTableItemType = _globalData.typesOfTools[value];
                    break;
                case MainMenuTypes.Workers:                    
                    temporaryList = Filter(_globalData.typesOfWorkers[value], _globalData.listOfWorkers);
                     MenuManager.instance.TemporaryTableItemType = _globalData.typesOfWorkers[value];
                    break;
                case MainMenuTypes.Parts:
                    temporaryList = Filter(_globalData.typesOfParts[value], _globalData.listOfParts);
                     MenuManager.instance.TemporaryTableItemType = _globalData.typesOfParts[value];
                    break;
                case MainMenuTypes.StatisticPart:
                    temporaryList = Filter(_globalData.typesOfParts[value], _globalData.listOfParts);
                    break;
                case MainMenuTypes.StatisticTool:
                    temporaryList = Filter(_globalData.typesOfTools[value], _globalData.listOfTools);
                    break;
                default:
                    break;
            }
             ShowTable(temporaryList);
            
        }
        private List<TableItem> Filter<T>(string type, List<T> list) where T : TableItem
        {
            var temporaryList = new List<TableItem>();
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
                UIPopupMessage.instance.ShowMessage($"{list} is empty");
                return;
            }            
            _tableManager.SetTableData(list);
        }
    }
}