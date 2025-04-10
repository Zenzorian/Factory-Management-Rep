using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Data;

[System.Serializable]
public class TableView : MonoBehaviour
{
    [Header("TABLE OBJECT")]
    [SerializeField] private Transform _tableObject;
    [Header("SCROLL RECT PREFAB")]
    [SerializeField] private GameObject _scrollViewObject;

    [Header("BACKGROUND")]
    [SerializeField] private Color _headerBackgroundColor;
    [SerializeField] private Color _tableBackgroundColor;

    [Header("CELL CREATOR")]
    [SerializeField] private TableCellCreator _cellCreator = new TableCellCreator();
    [Header("CELL SIZE")]
    [SerializeField] private float _height = 35;
    [SerializeField] private float _width = 200;
    [SerializeField] private bool _customFirstColumIsActive = false;
    [SerializeField] private GameObject _customFirstColum;
    [SerializeField] private float _customFirstColumWidth;

    [Header("PADDING")]
    [SerializeField] private RectOffset _headerRectOffset = new RectOffset();
    [SerializeField] private RectOffset _tableRectOffset = new RectOffset();
    [Header("SPACING")]
    [SerializeField] private float _horizontalSpacing = 5;
    [SerializeField] private float _verticalSpacing = 5;

    private ScrollRect _scrollRect;

    private Transform _statisticTableObject;

    private RectTransform _headerContainer;
    private RectTransform _tableContainer;    

    private float _totalRowsHeight;
    private float _totalColumnsWidth;

    private float _scrollbarWidth;
    private TableCell[,] _tableCells;

    private Table _table;
    public TableCell[,] GetTableCells()
    {
        return _tableCells;
    }
    public void OpenTable()
    {
        if (_tableObject != null)
            _tableObject.gameObject.SetActive(true);

    }
    public void CloseTable()
    {
        if (_tableObject != null)
            _tableObject.gameObject.SetActive(false);
    }

    public void CreateTable(Part part, Action action, Transform container, Action<PartCardData> CellClicked = null)
    {
        _statisticTableObject = container;
        if(_statisticTableObject == null) return;

        var headerFields = new List<string>();  
        foreach (var operation in part.Operations)
        {
            Debug.Log(operation.Name);
            headerFields.Add(operation.Name);
        }
        var table = new Table(headerFields.ToArray(), new string[0, 0], null); 
       
        CreateColumnBasedTable(table, action, part.Operations);
    }
    public void CreateTable(Table table)
    {
        ClearTable();
        _table = table;   

        CreateRowBasedTable(table);       
        
        OpenTable();
    }

     private void CreateRowBasedTable(Table table)
    {
        ClearTable();
        
        SetTableSize(table);

        if (_customFirstColumIsActive) FirstColumnWidth(table);
       
        _scrollbarWidth = _scrollViewObject.GetComponent<ScrollRect>().horizontalScrollbar.GetComponent<RectTransform>().sizeDelta.x;

        _headerContainer = CreateHeaderContainer(_tableObject);

        _tableContainer = CreateTableContainer(_tableObject);

        SetScrollRectSettings();

        CreateHeaderColumns(table.HeaderFields, _customFirstColumIsActive);

        CreateTableColumns(table.TableCells);

        _scrollRect.content.GetComponent<VerticalLayoutGroup>().padding = _tableRectOffset;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_headerContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_tableContainer);

    }   

    private void CreateColumnBasedTable(Table table, Action action, List<Operation> operations)
    {
        ClearTable();

        Debug.Log("CreateColumnBasedTable");

        SetTableSize(table);
        
        //if (_customFirstColumIsActive) FirstColumnWidth(table);

        _scrollbarWidth = _scrollViewObject.GetComponent<ScrollRect>().horizontalScrollbar.GetComponent<RectTransform>().sizeDelta.x;

        _headerContainer = CreateHeaderContainer(_statisticTableObject);

        _tableContainer = CreateTableContainer(_statisticTableObject);
       
        SetScrollRectSettings();

        var layoutGroup = AddHorizontalLayoutGroup(_scrollRect.content.gameObject);
        layoutGroup.padding.right = (int)_scrollbarWidth;


        // GameObject horizontalContainer = new GameObject("HorizontalContainer");        
        // RectTransform horizontalRect = horizontalContainer.AddComponent<RectTransform>();
        // horizontalRect.SetParent(_tableContainer);
        // horizontalRect.localScale = Vector3.one;
        // horizontalRect.anchorMin = new Vector2(0, 0);
        // horizontalRect.anchorMax = new Vector2(1, 1);
        // horizontalRect.sizeDelta = Vector2.zero;
        // horizontalRect.anchoredPosition = Vector2.zero;
        
        // // Добавляем HorizontalLayoutGroup
        // HorizontalLayoutGroup horizontalLayout = AddHorizontalLayoutGroup(horizontalContainer);
        // horizontalLayout.padding = _tableRectOffset;
        // horizontalLayout.childForceExpandHeight = true;

        CreateHeaderColumns(table.HeaderFields, false);
        
        // Создаем колонки
        // for (int c = 0; c < columns; c++)
        // {
        //     // Создаем вертикальный контейнер для колонки
        //     GameObject columnContainer = new GameObject($"Column_{c}");
        //     RectTransform columnRect = columnContainer.AddComponent<RectTransform>();
        //     columnRect.SetParent(horizontalRect);
        //     columnRect.localScale = Vector3.one;
            
        //     // Учитываем кастомную первую колонку если она активна
        //     float columnWidth = (c == 0 && _customFirstColumIsActive) ? _customFirstColumWidth : _width;
        //     columnRect.sizeDelta = new Vector2(columnWidth, 0);
            
        //     // Добавляем VerticalLayoutGroup
        //     VerticalLayoutGroup verticalLayout = AddVerticallLayoutGroup(columnContainer);
        //     verticalLayout.childForceExpandWidth = true;
            
        //     // Добавляем заголовок колонки в виде ячейки
        //     var headerCell = _cellCreator.CreateCell(parent: columnRect, itsHeader: true);
        //     headerCell.text.text = table.HeaderFields[c];
        //     headerCell.rectTransform.sizeDelta = new Vector2(columnWidth, _height);
        //     headerCell.image.color = _headerBackgroundColor;
            
        //     // Создаем ячейки данных для колонки
        //     for (int r = 0; r < rows; r++)
        //     {
        //         TableCell cell;
                
        //         // Используем кастомную ячейку если это первая колонка и кастомная колонка активна
        //         if (c == 0 && _customFirstColumIsActive)
        //         {
        //             cell = _cellCreator.CreateCell(parent: columnRect, customTableCell: _customFirstColum);
        //             cell.rectTransform.sizeDelta = new Vector2(_customFirstColumWidth, _height);
        //         }
        //         else
        //         {
        //             cell = _cellCreator.CreateCell(parent: columnRect);
        //             cell.rectTransform.sizeDelta = new Vector2(_width, _height);
        //             cell.image.color = _tableBackgroundColor;
        //         }
                
        //         cell.text.text = table.TableCells[r, c];
        //         cell.rectTransform.localScale = Vector3.one;
                
        //         int currentRow = r;
        //         cell.rectTransform.GetComponent<Button>().onClick.AddListener(() => _table.OnCellClicked(currentRow));
                
        //         // Сохраняем ячейку в массиве
        //         _tableCells[r, c] = cell;
        //     }
        // }
        CreateAddOperationButton(_headerContainer, action);
        // Обновляем лейаут
        //LayoutRebuilder.ForceRebuildLayoutImmediate(horizontalRect);
    }

    private void SetTableSize(Table table)
    {
        int rows = table.TableCells.GetLength(0);
        int columns = table.TableCells.GetLength(1);

        _tableCells = new TableCell[rows, columns];

        _totalRowsHeight = rows * (_height + _verticalSpacing);
        _totalColumnsWidth = columns * (_width + _horizontalSpacing);
    }

    private void FirstColumnWidth(Table table)
    {
        var rectWidth = _tableObject.GetComponent<RectTransform>().rect.width;
        _totalColumnsWidth = (table.TableCells.GetLength(1) - 1) * (_width + _horizontalSpacing) + _customFirstColumWidth;
        if (_totalColumnsWidth < rectWidth)
            _totalColumnsWidth = rectWidth;
    }

    private void CreateAddOperationButton(Transform parent, Action action)
    {
        var addOperationButton = _cellCreator.CreateCell(parent: parent, itsHeader: true);
        addOperationButton.text.text = "+";
        var button = addOperationButton.rectTransform.gameObject.AddComponent<Button>();
        addOperationButton.rectTransform.gameObject.GetComponent<Image>().raycastTarget = true;
        button.onClick.AddListener(() => action());
    }
   
    private RectTransform CreateHeaderContainer(Transform parent)
    {
        var headerHeight = _height + _headerRectOffset.top + _headerRectOffset.bottom;
        var headerWidth = _totalColumnsWidth + _scrollbarWidth;
        var headerSize = new Vector2(headerWidth, headerHeight);

        var header = new GameObject("Table Header");

        var headerRect = header.AddComponent<RectTransform>();
        headerRect.SetParent(parent);
        headerRect.sizeDelta = headerSize;
        headerRect.anchorMin = new Vector2(0, 1);
        headerRect.anchorMax = new Vector2(0, 1);
        headerRect.pivot = new Vector2(0, 1);
        headerRect.anchoredPosition = Vector2.zero;
        headerRect.localScale = Vector3.one;
        headerRect.SetAsFirstSibling();

        var headerImage = header.AddComponent<Image>();
        headerImage.color = _headerBackgroundColor;

        var layoutGroup = AddHorizontalLayoutGroup(header);

        layoutGroup.padding.right = _headerRectOffset.right + (int)_scrollbarWidth;
        layoutGroup.padding.top = _headerRectOffset.top;
        layoutGroup.padding.left = _headerRectOffset.left;
        layoutGroup.padding.bottom = _headerRectOffset.bottom;

        return headerRect;
    }

    private RectTransform CreateTableContainer(Transform parent)
    {
        var table = Instantiate(_scrollViewObject);

        var tableRect = table.GetComponent<RectTransform>();
        tableRect.SetParent(parent);
        tableRect.anchorMin = new Vector2(0, 0);
        tableRect.anchorMax = new Vector2(1, 1);
        tableRect.localScale = Vector3.one;
        tableRect.offsetMax = new Vector2(0, -_headerContainer.sizeDelta.y);
        tableRect.offsetMin = Vector2.zero;
        tableRect.SetAsFirstSibling();

        var tableImage = table.GetComponent<Image>();
        tableImage.color = _tableBackgroundColor;

        return tableRect;
    }

    private void SetScrollRectSettings()
    {
        _scrollRect = _tableContainer.GetComponent<ScrollRect>();
        _scrollRect.horizontalScrollbar.onValueChanged.AddListener(HandleHorizontalScroll);

        var content = _scrollRect.content;
        content.anchorMin = new Vector2(0, 1);
        content.anchorMax = new Vector2(0, 1);
        content.pivot = new Vector2(0, 1);
        content.anchoredPosition = Vector2.zero;
        content.localScale = Vector3.one;
        content.sizeDelta = new Vector2(_totalColumnsWidth, _totalRowsHeight);
    }

    private void CreateHeaderColumns(string[] fields, bool isCustomFirstColumnActive)
    {
        foreach (var item in fields)
        {
            var cell = _cellCreator.CreateCell(parent: _headerContainer, itsHeader: true);
            cell.text.text = item;
            cell.rectTransform.sizeDelta = new Vector2(_width, _height);

            if (isCustomFirstColumnActive == true && fields[0] == item)
                cell.rectTransform.sizeDelta = new Vector2(_customFirstColumWidth, _height);
        }
    }

    private void CreateTableColumns(string[,] tableData)
    {
        int rows = tableData.GetLength(0);
        int columns = tableData.GetLength(1);

        var layoutGroup = AddVerticallLayoutGroup(_scrollRect.content.gameObject);
        layoutGroup.padding.right = (int)_scrollbarWidth;

        for (int r = 0; r < rows; r++)
        {
            var row = AddRow(_scrollRect.content);
            AddHorizontalLayoutGroup(row.gameObject);
            for (int c = 0; c < columns; c++)
            {
                TableCell cell = new TableCell();
                if (_customFirstColumIsActive == true && c == 0)
                {
                    cell = _cellCreator.CreateCell(parent: row, customTableCell: _customFirstColum);
                    cell.rectTransform.sizeDelta = new Vector2(_customFirstColumWidth, _height);
                }
                else
                {
                    cell = _cellCreator.CreateCell(parent: row);
                    cell.rectTransform.sizeDelta = new Vector2(_width, _height);
                }
                cell.text.text = tableData[r, c];
                cell.rectTransform.localScale = Vector3.one;

                int currentRow = r;

                _tableCells[r, c] = cell;
                _tableCells[r, c].rectTransform = cell.rectTransform;

                cell.rectTransform.GetComponent<Button>().onClick.AddListener(() => _table.OnCellClicked(currentRow));
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
        rect.height = _height;

        return rectTransform;
    }
    private HorizontalLayoutGroup AddHorizontalLayoutGroup(GameObject container)
    {
        if (container.GetComponent<HorizontalLayoutGroup>() == true)
            return container.GetComponent<HorizontalLayoutGroup>();

        var layoutCroup = container.AddComponent<HorizontalLayoutGroup>();
        layoutCroup.spacing = _horizontalSpacing;
        layoutCroup.childControlHeight = true;
        layoutCroup.childControlWidth = false;
        layoutCroup.childForceExpandWidth = false;
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
        layoutCroup.childControlHeight = true;

        return layoutCroup;
    }
    public void ClearTable()
    {     
        if (_headerContainer != null)
            Destroy(_headerContainer.gameObject);
        if (_tableContainer != null)
            Destroy(_tableContainer.gameObject);
            
        _tableCells = null;
    }
    private void HandleHorizontalScroll(float value)
    {
        _headerContainer.anchoredPosition = new Vector2(value * (_headerContainer.rect.width - _scrollRect.viewport.rect.width) * -1, _headerContainer.anchoredPosition.y);
    }   
}
public class Table
{
    public Table(string[] headerFields, string[,] tableCells , Action<int> onCellClicked)
    {
        HeaderFields = headerFields;
        TableCells = tableCells;
        OnCellClicked = onCellClicked;
    }
    public string[] HeaderFields { get; private set; }
    public string[,] TableCells { get; private set; }
    public Action<int> OnCellClicked { get; set; }
}