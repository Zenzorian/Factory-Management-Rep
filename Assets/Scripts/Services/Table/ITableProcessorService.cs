using Scripts.Data;
using Scripts.Services;
using System;

public interface ITableProcessorService : IService
{       
    void OpenTable();
    public void CloseTable();
    TableCell[,] GetTableCells();   
    void SetTableData(MainMenuTypes menuType, int indexOfSelectedCategoty, Action<TableItem> OnCellClicked = null);
}
