using Scripts.Data;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TableView : MonoBehaviour, ITableView
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

    private RectTransform _headerContainer;
    private RectTransform _tableContainer;    

    private float _totalRowsHeight;
    private float _totalColumnsWidth;

    private float _scrollbarWidth;
    private TableCell[,] _tableCells;

    private List<TableItem> _tableItems;

    public TableCell[,] GetTableCells()
    {
        return _tableCells;
    }
    public void OpenTable()
    {
        _tableObject.parent.gameObject.SetActive(true);
    }
    public void CloseTable()
    {
        _tableObject.parent.gameObject.gameObject.SetActive(false);
    }
    public void SetTableData(List<TableItem> list)
    {
        if (list == null || list.Count == 0)
            return;

        _tableItems = list;
        FieldInfo[] fields = typeof(TableItem).GetFields();
        List<string> fieldNames = new List<string>();

        foreach (var field in fields)
        {
            fieldNames.Add(field.Name);
        }

        // Создаем массив для данных таблицы
        var tableData = new string[list.Count, fieldNames.Count];

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < fields.Length; j++)
            {
                var value = fields[j].GetValue(list[i]);
                tableData[i, j] = value != null ? value.ToString() : string.Empty;
            }
        }

        Table table = new Table(fieldNames.ToArray(), tableData);

        CreateTable(table);
    }
    public void CreateTable(Table table)
    {
        ClearTable();       

        _tableCells = new TableCell[table.TableCells.GetLength(0), table.TableCells.GetLength(1)];

        _totalRowsHeight = table.TableCells.GetLength(0) * (_height + _verticalSpacing);
        _totalColumnsWidth = table.TableCells.GetLength(1) * (_width + _horizontalSpacing);

        if (_customFirstColumIsActive)
        { 
            var rectWidth = _tableObject.GetComponent<RectTransform>().rect.width;
            _totalColumnsWidth = (table.TableCells.GetLength(1) -1) * (_width + _horizontalSpacing) + _customFirstColumWidth;
            if (_totalColumnsWidth < rectWidth)
                _totalColumnsWidth = rectWidth;
        }
        _scrollbarWidth = _scrollViewObject.GetComponent<ScrollRect>().horizontalScrollbar.GetComponent<RectTransform>().sizeDelta.x;

        _headerContainer = CreateHeaderContainer();

        _tableContainer = CreateTableContainer();

        SetScrollRectSettings();

        CreateHeaderColumns(table.HeaderFields);

        CreateTableColumns(table.TableCells);
       
        _scrollRect.content.GetComponent<VerticalLayoutGroup>().padding = _tableRectOffset;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_headerContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_tableContainer);
             
        OpenTable();
    }
    
    private RectTransform CreateHeaderContainer()
    {
        var headerHeight = _height + _headerRectOffset.top + _headerRectOffset.bottom;
        var headerWidth = _totalColumnsWidth + _scrollbarWidth;
        var headerSize = new Vector2(headerWidth, headerHeight);

        var header = new GameObject("Table Header");

        var headerRect = header.AddComponent<RectTransform>();
        headerRect.SetParent(_tableObject);
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

    private RectTransform CreateTableContainer()
    {
        var table = Instantiate(_scrollViewObject);

        var tableRect = table.GetComponent<RectTransform>();
        tableRect.SetParent(_tableObject);        
        tableRect.anchorMin = new Vector2(0, 0);
        tableRect.anchorMax = new Vector2(1, 1); 
        tableRect.localScale = Vector3.one;
        tableRect.offsetMax = new Vector2(0, -_headerContainer.sizeDelta.y);
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

    private void CreateHeaderColumns(string[] fields)
    {
        foreach (var item in fields)
        {
            var cell = _cellCreator.CreateCell(parent: _headerContainer, itsHeader: true);
            cell.text.text = item;
            cell.rectTransform.sizeDelta = new Vector2(_width, _height);

            if (_customFirstColumIsActive == true && fields[0] == item)                      
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
                    cell = _cellCreator.CreateCell(parent: row,customTableCell: _customFirstColum);
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
                
                //cell.rectTransform.GetComponent<Button>().onClick.AddListener(() => OnCellClicked(currentRow));
            }
        }
    }

    //private void OnCellClicked(int rowIndex)
    //{       
    //    TableItem item = _tableItems[rowIndex];

    //    var menuType = MenuManager.Instance.menuType;
    //    if(menuType !=MainMenuTypes.Statistic&&
    //        menuType != MainMenuTypes.StatisticTool&&
    //        menuType != MainMenuTypes.StatisticPart)
    //    MenuManager.Instance.OnTableItemEdit.Invoke(item, MenuManager.Instance.menuType);

    //    if (item is Part part)
    //    {
    //        MenuManager.Instance.OnPartSelected.Invoke(part);
    //    }
    //    else if (item is Tool tool)
    //    {
    //        MenuManager.Instance.OnToolSelected.Invoke(tool);
    //    }
    //}
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
        if (_headerContainer == null || _tableContainer == null) return;
        Destroy(_headerContainer.gameObject);
        Destroy(_tableContainer.gameObject);
    }
    private void HandleHorizontalScroll(float value)
    {
        _headerContainer.anchoredPosition = new Vector2(value * (_headerContainer.rect.width - _scrollRect.viewport.rect.width) * -1, _headerContainer.anchoredPosition.y);
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
