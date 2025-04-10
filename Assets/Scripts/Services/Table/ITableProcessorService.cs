using Scripts.Data;
using Scripts.Services;
using System;
using UnityEngine;

public interface ITableProcessorService : IService
{       
    void OpenTable();
    void CreateColumnBasedTable(Part part, Action onAdded, Transform container, Action<PartCardData> OnCellClicked = null);
    public void CloseTable();
    TableCell[,] GetTableCells();   
    void SetTableData(MainMenuTypes menuType, int indexOfSelectedCategoty, Action<TableItem> OnCellClicked = null);

}
