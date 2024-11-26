
using Scripts.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Scripts.Services
{
    public class TableProcessor : ITableProcessorService
    {
        private readonly ISaveloadDataService _saveLoadData;
        private readonly TableView _tableView;

        private List<TableItem> _tableItems;

        public TableProcessor(ISaveloadDataService saveLoadData, TableView tableView)
        {
            _saveLoadData = saveLoadData;
            _tableView = tableView;
        }

        public void SetTableData(List<TableItem> listOfTableItems)
        {
            _tableItems = listOfTableItems;
            FieldInfo[] fields = typeof(TableItem).GetFields();
            List<string> fieldNames = new List<string>();

            foreach (var field in fields)
            {
                fieldNames.Add(field.Name);
            }

            // Создаем массив для данных таблицы
            var tableData = new string[listOfTableItems.Count, fieldNames.Count];

            for (int i = 0; i < listOfTableItems.Count; i++)
            {
                for (int j = 0; j < fields.Length; j++)
                {
                    var value = fields[j].GetValue(listOfTableItems[i]);
                    tableData[i, j] = value != null ? value.ToString() : string.Empty;
                }
            }

            Table table = new Table(fieldNames.ToArray(), tableData);

            _tableView.CreateTable(table);
        }

        public TableCell[,] GetTableCells() => _tableView.GetTableCells();

        public void OpenTable() => _tableView.OpenTable();

        public void CloseTable() => _tableView.CloseTable();

    }    
}