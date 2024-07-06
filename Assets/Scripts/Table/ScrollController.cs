using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ScrollController : MonoBehaviour
{
    [SerializeField] private bool _isTouchScrollEnabled = true;
    [SerializeField] private float _touchScrollSensitivity;

    [SerializeField] private Scrollbar _horizontalScrollbar;
    [SerializeField] private Scrollbar _verticalScrollbar;

    private RectTransform _horizontalScrollView;
    private RectTransform _tableContainer;
    private float _verticalOffset;

    private Vector2 _lastTouchPosition;
    public void ScrollAdjuster(RectTransform horizontalScrollView, RectTransform tableContainer)
    {
        _horizontalScrollView = horizontalScrollView;
        _tableContainer = tableContainer;

        SetupScrollbars();

        _horizontalScrollbar.onValueChanged.AddListener(HandleHorizontalScroll);
        _verticalScrollbar.onValueChanged.AddListener(HandleVerticalScroll);
        _verticalOffset = _tableContainer.offsetMax.y;
    }

    private void SetupScrollbars()
    {
        var horizontalScrollbarRect = _horizontalScrollbar.GetComponent<RectTransform>();
        var verticalScrollbarRect = _verticalScrollbar.GetComponent<RectTransform>();

        if (horizontalScrollbarRect.rect.width >= _horizontalScrollView.rect.width || _isTouchScrollEnabled)
        {
            _horizontalScrollbar.gameObject.SetActive(false);
        }
        else
        {
            _horizontalScrollbar.gameObject.SetActive(true);
            _horizontalScrollbar.size = horizontalScrollbarRect.rect.width * 100 / _horizontalScrollView.rect.width / 100;
        }
        if (verticalScrollbarRect.rect.height >= _tableContainer.rect.height || _isTouchScrollEnabled)
        {
            _verticalScrollbar.gameObject.SetActive(false);
        }
        else
        {
            _verticalScrollbar.gameObject.SetActive(true);
            _verticalScrollbar.size = verticalScrollbarRect.rect.height * 100 / _tableContainer.rect.height / 100;
        }
    }

    private void HandleHorizontalScroll(float value)
    {
        _horizontalScrollView.anchoredPosition = new Vector2(value * (_horizontalScrollView.rect.width - _horizontalScrollView.parent.GetComponent<RectTransform>().rect.width) * -1, _horizontalScrollView.anchoredPosition.y);
    }

    private void HandleVerticalScroll(float value)
    {
        _tableContainer.anchoredPosition = new Vector2(_tableContainer.anchoredPosition.x, value * (_tableContainer.rect.height - _horizontalScrollView.parent.GetComponent<RectTransform>().rect.height + Mathf.Abs(_verticalOffset * 2)));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        if (!_isTouchScrollEnabled) return;
        _lastTouchPosition = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!_isTouchScrollEnabled) return;

        var delta = Vector2.Distance(eventData.position, _lastTouchPosition);
        // Пример прокрутки вертикального контента
        _verticalScrollbar.value = Mathf.Clamp((_verticalScrollbar.value - delta / (_verticalScrollbar.size * Screen.height)) / _touchScrollSensitivity, 0, 1);
        // Для горизонтальной прокрутки аналогично измените значение horizontalScrollbar

        _lastTouchPosition = eventData.position;
    }

}