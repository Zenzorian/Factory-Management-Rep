using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Data;
using Scripts.Services;

[System.Serializable]
public class TableView : MonoBehaviour
{
    [HideInInspector] public IButtonCreator buttonCreator;

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
    [SerializeField] private float _height = 120;
    [SerializeField] private float _width = 400;
    [SerializeField] private bool _customFirstColumIsActive = false;
    [SerializeField] private GameObject _customFirstColum;
    [SerializeField] private float _customFirstColumWidth;   

    [Header("PADDING")]
    [SerializeField] private RectOffset _headerRectOffset = new RectOffset();
    [SerializeField] private RectOffset _tableRectOffset = new RectOffset();
    [Header("SPACING")]
    [SerializeField] private float _horizontalSpacing = 15;
    [SerializeField] private float _verticalSpacing = 15;

    [Header("STATISTIC SPACER")]
    [SerializeField] private GameObject _statisticSpacer;
    [SerializeField] private float _statisticSpacerWidth = 60;
    [SerializeField] private float _statisticSpacerHeight = 60;    

    private ScrollRect _scrollRect;

    private Transform _statisticTableObject;

    private RectTransform _headerContainer;
    private RectTransform _tableContainer;    

    private float _totalRowsHeight;
    private float _totalColumnsWidth;

    private float _scrollbarWidth;
    private TableCell[,] _tableCells;

    private Table _table;

    private List<Button> _deleteButtons = new List<Button>();

    private Part _part;

    private bool _onEditMode = false;

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

    public void CreateTable(Part part, StatisticTableActions statisticTableActions, Transform container)
    {   
        ClearTable();

        _statisticTableObject = container;
        if(_statisticTableObject == null) return;       

        CreateColumnBasedTable(part, statisticTableActions);
    }
    public void CreateTable(Table table)
    {
        ClearTable();

        _table = table;   

        CreateRowBasedTable(table);       
        
        OpenTable();
    }

    public void ClearTable()
    {     
        if (_headerContainer != null)
            Destroy(_headerContainer.gameObject);
        if (_tableContainer != null)
            Destroy(_tableContainer.gameObject);
            
        _tableCells = null;
        _table = null;
        _totalRowsHeight = 0;
        _totalColumnsWidth = 0;
        _scrollRect = null;

        _deleteButtons.RemoveAll(button => button == null);
        _onEditMode = false;
    }

    public void SetEditMode()
    {
        _onEditMode = !_onEditMode;      
        
        foreach (var cell in _deleteButtons)
        {
            if (cell != null)
                cell.gameObject.SetActive(_onEditMode);
        }      
    }

    private void CreateRowBasedTable(Table table)
    {   
        SetTableSize(table.TableCells.GetLength(0), table.TableCells.GetLength(1));

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

    private void CreateColumnBasedTable(Part part, StatisticTableActions statisticTableActions)
    {  
        Debug.Log("CreateColumnBasedTable");   

        _part = part;

        var headerFields = new List<string>();  
        int maxStatisticsCount = 0;
        
        foreach (var operation in part.Operations)
        {            
            headerFields.Add(operation.Name);
                      
            if (operation.Statistics.Count > maxStatisticsCount)
                maxStatisticsCount = operation.Statistics.Count;
        }  

        int spacersCountHorizontal = part.Operations.Count - 1;
        int additionalColumns = Mathf.CeilToInt(spacersCountHorizontal * (_statisticSpacerWidth / _width));

        int spacersCountVertical = maxStatisticsCount - 1;
        int additionalRows = Mathf.CeilToInt(spacersCountVertical * (_statisticSpacerHeight / _height));

        SetTableSize(maxStatisticsCount+1 + additionalRows, part.Operations.Count+1 + additionalColumns);
        
        //if (_customFirstColumIsActive) FirstColumnWidth(table);

        _scrollbarWidth = _scrollViewObject.GetComponent<ScrollRect>().horizontalScrollbar.GetComponent<RectTransform>().sizeDelta.x;

        _headerContainer = CreateHeaderContainer(_statisticTableObject, true);

        _tableContainer = CreateTableContainer(_statisticTableObject);
       
        SetScrollRectSettings();
        
        CreateHeaderColumns(headerFields.ToArray(), false,true, statisticTableActions.onDeleteOperation);
             
        CreateTableColumnsForColumnBasedTable(part, statisticTableActions);

        _scrollRect.horizontal = true;

        var addButton = _cellCreator.CreateCell(parent: _headerContainer, itsHeader: true);
        addButton.text.text = "Add Operation";
        addButton.rectTransform.sizeDelta = new Vector2(_width, _height);
        var button = addButton.rectTransform.gameObject.AddComponent<Button>();
        addButton.rectTransform.gameObject.GetComponent<Image>().raycastTarget = true;
        button.onClick.AddListener(() => statisticTableActions.OnAddOperationButtonClicked());
       
        LayoutRebuilder.ForceRebuildLayoutImmediate(_headerContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_tableContainer);
        
        // Принудительное обновление скролла для исправления бага
        Canvas.ForceUpdateCanvases();
        _scrollRect.enabled = false;
        _scrollRect.enabled = true;
    }

    private void SetTableSize(int rows, int columns)
    {  
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
  
    private RectTransform CreateHeaderContainer(Transform parent, bool isColumnBasedTable = false)
    {
        var headerHeight = _height + _headerRectOffset.bottom;
        headerHeight+= isColumnBasedTable ? _headerRectOffset.bottom : _headerRectOffset.top;      

        var headerWidth = _totalColumnsWidth + _scrollbarWidth + _headerRectOffset.right + _headerRectOffset.left;
              
        float screenWidth = Screen.width;
               
        if (headerWidth < screenWidth)headerWidth = screenWidth;        
        
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
        layoutGroup.padding.top = isColumnBasedTable ? _headerRectOffset.bottom : _headerRectOffset.top;
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
        content.sizeDelta = new Vector2(_totalColumnsWidth +_tableRectOffset.left, _totalRowsHeight);
    }

    private void CreateHeaderColumns(string[] fields, bool isCustomFirstColumnActive, bool isColumnBasedTable = false, Action<PartCardData> onDeleteOperation = null)
    {
        foreach (var item in fields)
        {
            var cell = _cellCreator.CreateCell(parent: _headerContainer, itsHeader: true);
            cell.text.text = item;
            cell.rectTransform.sizeDelta = new Vector2(_width, _height);

            if (isCustomFirstColumnActive == true && fields[0] == item)
                cell.rectTransform.sizeDelta = new Vector2(_customFirstColumWidth, _height);

            if (isColumnBasedTable == true)
            {
                CreateStatisticSpacer(_headerContainer, arrowChar:'→');

                var deleteButton = buttonCreator.CreateDeleteButton(cell.rectTransform);
                _deleteButtons.Add(deleteButton);     
                var partCardData = new PartCardData(_part, _part.Operations[Array.IndexOf(fields, item)], null);           
                deleteButton.onClick.AddListener(() => onDeleteOperation(partCardData));
            }
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
    private void CreateTableColumnsForColumnBasedTable(Part part, StatisticTableActions statisticTableActions)
    {        
        var layoutGroup = AddHorizontalLayoutGroup(_scrollRect.content.gameObject);    
        layoutGroup.spacing = _horizontalSpacing *2 + _statisticSpacerWidth;
        layoutGroup.padding = _tableRectOffset;
        layoutGroup.padding.right += (int)_scrollbarWidth;
        
        foreach (var operation in part.Operations)
        {
            var column = AddColumn(layoutGroup.transform);
            var columnGroup = AddVerticallLayoutGroup(column.gameObject);
           
           foreach (var statistic in operation.Statistics)
           {
                CreateStatisticSpacer(column.transform, arrowChar:'↓');

                TableCell cell = new TableCell();
               
                cell = _cellCreator.CreateCell(parent: column.transform);
                cell.rectTransform.sizeDelta = new Vector2(_width, _height);

                cell.text.text = $"Tool: {statistic.Tool.Name} \nProcessing Type: {statistic.ProcessingType}";
                cell.rectTransform.localScale = Vector3.one;

                var partCardData = new PartCardData(part, operation, statistic);

                cell.rectTransform.GetComponent<Button>().onClick.AddListener(() => statisticTableActions.OnCellClicked(partCardData));
            
                var deleteButton = buttonCreator.CreateDeleteButton(cell.rectTransform);
                _deleteButtons.Add(deleteButton);
                deleteButton.onClick.AddListener(() => statisticTableActions.onDeleteStatisticButtonClicked(partCardData));

           }
            CreateStatisticSpacer(column.transform, arrowChar:'↓');
            CreateAddToolButton(column.transform, statisticTableActions.OnAddToolButtonClicked, "Add Tool", operation);
        }  
    }
   
    private RectTransform AddRow(Transform container)
    {
        var row = new GameObject("Row");
        var rectTransform = row.AddComponent<RectTransform>();
        row.transform.SetParent(container);

        rectTransform.localScale = Vector3.one;

        var rect = rectTransform.rect;
        rect.height = _height;

        return rectTransform;
    }
     
    private RectTransform AddColumn(Transform container)
    {
        var column = new GameObject("Column");
        var rectTransform = column.AddComponent<RectTransform>();
        column.transform.SetParent(container);

        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta = new Vector2(_width, 0);

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
        layoutCroup.childControlHeight = false;

        return layoutCroup;
    }
   
    private void HandleHorizontalScroll(float value)
    {
        _headerContainer.anchoredPosition = new Vector2(value * (_headerContainer.rect.width - _scrollRect.viewport.rect.width) * -1, _headerContainer.anchoredPosition.y);
    }   

    private void CreateAddOperationButton(Transform parent, Action OnAddOperationButtonClicked, string buttonText)
    {
        var button = CreateAddButton(parent, buttonText, true);
        button.onClick.AddListener(() => OnAddOperationButtonClicked());
    }
    private void CreateAddToolButton(Transform parent, Action<Operation> OnAddToolClicked, string buttonText, Operation operation)
    {
        var button = CreateAddButton(parent, buttonText, true);
        button.onClick.AddListener(() => OnAddToolClicked(operation));
    }
    private Button CreateAddButton(Transform parent, string buttonText, bool isHeader = false)
    {
        var addButton = _cellCreator.CreateCell(parent: parent, itsHeader: isHeader);
        addButton.text.text = buttonText;
        addButton.rectTransform.sizeDelta = new Vector2(_width, _height);
        var button = addButton.rectTransform.gameObject.AddComponent<Button>();
        addButton.rectTransform.gameObject.GetComponent<Image>().raycastTarget = true;
        
        return button;
    }
    private void CreateStatisticSpacer(Transform parent,char arrowChar)
    {
        var spacerObject = Instantiate(_statisticSpacer);
        var statisticSpacer = spacerObject.GetComponent<StatisticSpacerElements>();

        statisticSpacer.transform.SetParent(parent);
        statisticSpacer.rectTransform.sizeDelta = new Vector2(_statisticSpacerWidth, _statisticSpacerHeight); 
        statisticSpacer.text.text = arrowChar.ToString();
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
public class StatisticTableActions
{
   public Action OnAddOperationButtonClicked;
   public Action<Operation> OnAddToolButtonClicked;
   public Action<PartCardData> OnCellClicked; 
   public Action<PartCardData> onDeleteOperation;
   public Action<PartCardData> onDeleteStatisticButtonClicked;

   public StatisticTableActions
   (
    Action OnAddOperationButtonClicked,
    Action<Operation> OnAddToolButtonClicked,
    Action<PartCardData> OnCellClicked,
    Action<PartCardData> onDeleteOperation,
    Action<PartCardData> onDeleteStatisticButtonClicked
    )
   {
        this.OnAddOperationButtonClicked = OnAddOperationButtonClicked;
        this.OnAddToolButtonClicked = OnAddToolButtonClicked;
        this.OnCellClicked = OnCellClicked;
        this.onDeleteOperation = onDeleteOperation;
        this.onDeleteStatisticButtonClicked = onDeleteStatisticButtonClicked;
   }
}
