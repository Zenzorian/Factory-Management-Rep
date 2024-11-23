using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TableCellCreator
{
    [Header("CELLS PREFABS")]
    [SerializeField] private GameObject _headerCell;
    [SerializeField] private GameObject _tableCell;
    //[Header("TEXT SETTINGS")]   
    //[SerializeField] private Vector4 _textPadding;    
    public TableCell CreateCell(Transform parent, GameObject customTableCell = null, bool itsHeader = false)
    {       
        if (itsHeader == true) return CellAdjuster(parent,_headerCell, "Header Cell");       
        else if(customTableCell != null) return CellAdjuster(parent, customTableCell, "Custom Table Cell");
        else return CellAdjuster(parent, _tableCell, "Table Cell"); 
    }
    private TableCell CellAdjuster(Transform parent, GameObject cellPrefab,string cellName)
    {
        TableCell tableCell = new TableCell();

        var cell = GameObject.Instantiate(cellPrefab, parent);
        cell.name = cellName;

        tableCell.rectTransform = cell.GetComponent<RectTransform>(); ;
        tableCell.image = cell.GetComponent<Image>();

        foreach (Transform item in cell.transform)
        {
            var textRect = item.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            
            tableCell.text = item.GetComponent<Text>();
        }  

        return tableCell;
    }
}
public class TableCell
{
    public RectTransform rectTransform;
    public Text text;
    public Image image;
}
