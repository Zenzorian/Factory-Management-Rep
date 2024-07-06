using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TableCellCreator
{
    [SerializeField] private GameObject _headerCell;
    [SerializeField] private GameObject _tableCell;
    [Header("TEXT SETTINGS")]   
    [SerializeField] private Vector4 _textPadding;    
    public TableCell CreateCell(Transform parent, bool itsHeader = false)
    {       
        if (itsHeader == true) return CellAdjuster(parent,_headerCell);       
        else return CellAdjuster(parent, _tableCell); 
    }
    private TableCell CellAdjuster(Transform parent, GameObject cellPrefab)
    {
        TableCell tableCell = new TableCell();

        var cell = GameObject.Instantiate(cellPrefab, parent);
        
        tableCell.rectTransform = cell.GetComponent<RectTransform>(); ;
        tableCell.image = cell.GetComponent<Image>();

        foreach (Transform item in cell.transform)
        {
            var textRect = item.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(_textPadding.x, _textPadding.w);//_offset.w низ _offset.x лево
            textRect.offsetMax = new Vector2(_textPadding.z * (-1), _textPadding.y * (-1)); //_offset.z право _offset.y верх

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
