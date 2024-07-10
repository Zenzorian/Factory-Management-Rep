using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TableView : MonoBehaviour
{
    [SerializeField] private RectTransform _tableRect;
    [SerializeField] private TableCellCreator _cellCreator = new TableCellCreator();
    [Header("CONTAINERS")]
    [SerializeField] private RectTransform _headerContainer;
    [SerializeField] private RectTransform _tableContainer;
    [Header("SPACING")]
    [SerializeField] private float _horizontalSpacing = 5;
    [SerializeField] private float _verticalSpacing = 5;

    [SerializeField] private float _rowHeight = 35;
    [SerializeField] private float _columnWidth = 200;
    //[SerializeField] private Vector4 _offset = new Vector4(5,5,25,5);
    [Header("SCROLL VIEW")]
    [SerializeField] private ScrollRect _scrollRect;

    private List<TableCell> _headerColumn = new List<TableCell>();    
            
    public void CreateTable(Table table)
    {  
        float totalRowsHeight = table.TableCells.GetLength(0) * (_rowHeight + _verticalSpacing);
        float totalColumnsWidth = table.TableCells.GetLength(1) * (_columnWidth + _horizontalSpacing);        

        _tableContainer.sizeDelta = new Vector2(totalColumnsWidth, totalRowsHeight);
        _headerContainer.sizeDelta = new Vector2(totalColumnsWidth, _rowHeight);

        CreateHeaderColumns(table.HeaderFields);

        CreateTableColumns(table.TableCells);

        _scrollRect.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, (_rowHeight + _verticalSpacing) * -1);

        _scrollRect.horizontalScrollbar.onValueChanged.AddListener(HandleHorizontalScroll);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_headerContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_tableContainer);
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

        var scrolWidth = _scrollRect.verticalScrollbar.transform.GetComponent<RectTransform>().rect.width;       
        var layoutGroup = AddVerticallLayoutGroup(_tableContainer.gameObject);

        layoutGroup.padding.right = (int)scrolWidth;

        for (int r = 0; r < rows; r++)
        {
            var row = AddRow(_tableContainer);
            AddHorizontalLayoutGroup(row.gameObject);
            for (int c = 0; c < columns; c++)
            {
                var cell = _cellCreator.CreateCell(row);
                cell.rectTransform.sizeDelta = new Vector2(0, _rowHeight);
                cell.text.text = tableData[r, c];
               
                cell.rectTransform.localScale = Vector3.one;
            }
        }
    }
    public RectTransform AddRow(Transform container)
    {
        var row = new GameObject("Row");
        var rectTransform = row.AddComponent<RectTransform>();
        row.transform.SetParent(container);
                
        rectTransform.localScale = Vector3.one;
                
        var rect = rectTransform.rect;
        rect.height = _rowHeight;

        return rectTransform;
    }
    private HorizontalLayoutGroup AddHorizontalLayoutGroup(GameObject container)
    {
        if (container.GetComponent<HorizontalLayoutGroup>() == true) 
            return container.GetComponent<HorizontalLayoutGroup>();

        var layoutCroup = container.AddComponent<HorizontalLayoutGroup>();
        layoutCroup.spacing = _horizontalSpacing;
        layoutCroup.childControlHeight = false;
        layoutCroup.childControlWidth = true;
        return layoutCroup;
    }
    private VerticalLayoutGroup AddVerticallLayoutGroup(GameObject container)
    {
        if (container.GetComponent<VerticalLayoutGroup>() == true)
            return container.GetComponent<VerticalLayoutGroup>(); ;

        var layoutCroup = container.AddComponent<VerticalLayoutGroup>();
        layoutCroup.spacing = _verticalSpacing;
        layoutCroup.childControlHeight = false;
        layoutCroup.childForceExpandHeight = false;

        return layoutCroup;
    }
    public void ClearTable()
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
        _headerContainer.anchoredPosition = new Vector2(value * (_headerContainer.rect.width - _tableRect.rect.width) * -1, _headerContainer.anchoredPosition.y);
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
