using Scripts.Data;
using Scripts.Services;
using System;
using UnityEngine;

public interface ITableProcessorService : IService
{       
    void OpenTable();
    void CreateColumnBasedTable(Part part, StatisticTableActions statisticTableActions, Transform container);
    public void CloseTable();   
    TableCell[,] GetTableCells();   
    void SetTableData(MainMenuTypes menuType, int indexOfSelectedCategoty, Action<TableItem> OnCellClicked = null);
    void SetEditMode();
}
