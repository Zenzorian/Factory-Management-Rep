using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TableManager : MonoBehaviour
{
    [SerializeField] private TableCellCreator _cellCreator = new TableCellCreator();
    [Header("CONTAINERS")]
    [SerializeField] private RectTransform _headerContainer;
    [SerializeField] private RectTransform _tableContainer;
    [Header("SPACING")]
    [SerializeField] private float _horizontalSpacing = 5;
    [SerializeField] private float _verticalSpacing = 5;

    [SerializeField] private float _rowHeight = 35;
    [SerializeField] private float _columnWidth = 200;
    [SerializeField] private Vector4 _offset = new Vector4(5,5,25,5);
    [Header("SCROLL VIEW")]
    [SerializeField] private ScrollRect _scrollRect;

    private List<TableCell> _headerColumn = new List<TableCell>();    
        
    public void CreateTable(Table table)
    {
        ClearTable();

        float totalRowsHeight = table.TableCells.GetLength(0) * (_rowHeight + _verticalSpacing);
        float totalColumnsWidth = table.TableCells.GetLength(1) * (_columnWidth + _horizontalSpacing);        

        _tableContainer.sizeDelta = new Vector2(totalColumnsWidth, totalRowsHeight);
        _headerContainer.sizeDelta = new Vector2(totalColumnsWidth, _rowHeight);

        CreateHeaderColumns(table.HeaderFields);

        CreateTableColumns(table.TableCells);

        _scrollRect.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, (_rowHeight + _verticalSpacing) * -1);

        _scrollRect.horizontalScrollbar.onValueChanged.AddListener(HandleHorizontalScroll);
    }
    private void CreateHeaderColumns(string[] fields)
    {
        var layoutGroup = AddHorizontalLayoutGroup(_headerContainer.gameObject);

        _headerColumn.Clear();

        var scrolWidth = _scrollRect.verticalScrollbar.transform.GetComponent<RectTransform>().rect.width;
        layoutGroup.padding.right = (int)scrolWidth;

        foreach (var item in fields)       
        {
            var cell = _cellCreator.CreateCell(_headerContainer,true);
            cell.text.text = item;
            cell.rectTransform.sizeDelta = new Vector2(0, _rowHeight);
            _headerColumn.Add(cell);
        }
    }
    private void CreateTableColumns(string[,] tableData)
    {
        int rows = tableData.GetLength(0);
        int columns = tableData.GetLength(1);

        AddVerticallLayoutGroup(_tableContainer.gameObject);

        for (int r = 0; r < rows; r++)
        {
            var row = AddRow(_tableContainer);
            AddHorizontalLayoutGroup(row.gameObject);
            for (int c = 0; c < columns; c++)
            {
                var cell = _cellCreator.CreateCell(row);
                cell.rectTransform.sizeDelta = new Vector2(0, _rowHeight);
            }
        }
    }
    public RectTransform AddRow(Transform container)
    {
        var row = new GameObject("Row");
       
        var rect = row.AddComponent<RectTransform>();       
        rect.sizeDelta = new Vector2(0, _rowHeight);
        row.transform.SetParent(container);
        return rect;
    }
    private HorizontalLayoutGroup AddHorizontalLayoutGroup(GameObject container)
    {
        if (container.GetComponent<HorizontalLayoutGroup>() == true) 
            return container.GetComponent<HorizontalLayoutGroup>();

        var layoutCroup = container.AddComponent<HorizontalLayoutGroup>();
        layoutCroup.spacing = _horizontalSpacing;
        layoutCroup.childControlHeight = false;
        //layoutCroup.childControlWidth = false;
        return layoutCroup;
    }
    private void AddVerticallLayoutGroup(GameObject container)
    {
        if (container.GetComponent<VerticalLayoutGroup>() == true) return;

        var layoutCroup = container.AddComponent<VerticalLayoutGroup>();
        layoutCroup.spacing = _verticalSpacing;
        layoutCroup.childControlHeight = false;
        layoutCroup.childForceExpandHeight = false;        
    }
    private void ClearTable()
    {
        foreach (Transform item in _headerContainer)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in _tableContainer)
        {
            Destroy(item.gameObject);
        }
    }
    private void HandleHorizontalScroll(float value)
    {
        _headerContainer.anchoredPosition = new Vector2(value * (_headerContainer.rect.width - transform.GetComponent<RectTransform>().rect.width) * -1, _headerContainer.anchoredPosition.y);
    }
}
public class Table
{
    public Table(string[] headerFields, string[,] tableCells)
    {
        HeaderFields = headerFields;
        TableCells = tableCells;
    }
    public string[] HeaderFields { get; private set;}
    public string[,] TableCells { get; private set; }
}
