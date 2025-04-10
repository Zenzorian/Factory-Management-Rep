using Scripts.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Scripts.Services
{
    public class TableProcessor : ITableProcessorService
    {
        private readonly ISaveloadDataService _saveLoadData;
        private readonly TableView _tableView;

        private List<TableItem> _tableItems;

        private Action<TableItem> _CellClicked;
        public TableProcessor(ISaveloadDataService saveLoadData, TableView tableView)
        {
            _saveLoadData = saveLoadData;
            _tableView = tableView;
        }

        public void SetTableData(MainMenuTypes menuType, int indexOfSelectedCategoty, Action<TableItem> CellClicked = null) 
        {
            _tableItems = _saveLoadData.GetItemsListWithFilter(menuType, indexOfSelectedCategoty);

            FieldInfo[] fields = typeof(TableItem).GetFields();
            List<string> fieldNames = new List<string>();

            foreach (var field in fields)
            {
                fieldNames.Add(field.Name);
            }
                       
            var tableData = new string[_tableItems.Count, fieldNames.Count];

            for (int i = 0; i < _tableItems.Count; i++)
            {
                for (int j = 0; j < fields.Length; j++)
                {
                    var value = fields[j].GetValue(_tableItems[i]);
                    tableData[i, j] = value != null ? value.ToString() : string.Empty;
                }
            }
            _CellClicked = CellClicked;
            Table table = new Table(fieldNames.ToArray(), tableData, OnCellClicked);

            _tableView.CreateTable(table);
        }
        public void CreateColumnBasedTable(Part part, Action action, Transform container, Action<PartCardData> CellClicked = null)
        {
            _tableView.CreateTable(part,action, container, CellClicked);            
        }

        private void OnCellClicked(int rowIndex)
        {
            TableItem item = _tableItems[rowIndex];
            _CellClicked?.Invoke(item);
        }

        public TableCell[,] GetTableCells() => _tableView.GetTableCells();

        public void OpenTable() => _tableView.OpenTable();

        public void CloseTable() => _tableView.CloseTable();
      
                   
    }
}    
