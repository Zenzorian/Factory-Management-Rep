using Scripts.Data;
using Scripts.Services;
using System;
using UnityEngine;

public interface ITableProcessorService : IService
{       
    void OpenTable();
    void CreateColumnBasedTable(Part part, Action OnAddOperationButtonClicked, Action<Operation> OnAddToolButtonClicked, Transform container, Action<PartCardData> onCellClicked = null);
    public void CloseTable();   
    TableCell[,] GetTableCells();   
    void SetTableData(MainMenuTypes menuType, int indexOfSelectedCategoty, Action<TableItem> OnCellClicked = null);

}
