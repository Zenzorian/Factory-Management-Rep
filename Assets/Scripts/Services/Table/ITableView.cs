using Scripts.Services;

public interface ITableView : IService
{
    void CreateTable(Table table);   
    void OpenTable();
    public void CloseTable();
    TableCell[,] GetTableCells();
}
