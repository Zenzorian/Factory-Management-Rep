using Scripts.Data;
using Scripts.Services;
using System.Collections.Generic;

public interface ITableProcessorService : IService
{    
    void SetTableData(List<TableItem> tableItems);
    void OpenTable();
    public void CloseTable();
    TableCell[,] GetTableCells();
}
