using UnityEngine;

public class SwipeRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f; // Скорость вращения объекта

    private Vector2 _startTouchPosition;
    private Vector2 _currentTouchPosition;
    private bool _isSwiping;

    void Update()
    {
        // Проверяем, если касание на экране
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                // Начало касания
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    _isSwiping = true;
                    break;

                // Перемещение пальца по экрану
                case TouchPhase.Moved:
                    if (_isSwiping)
                    {
                        _currentTouchPosition = touch.position;

                        // Разница в положении касания
                        Vector2 swipeDelta = _currentTouchPosition - _startTouchPosition;

                        // Вращаем объект по осям Y и X, основанное на горизонтальном и вертикальном свайпах
                        float rotationAmountY = swipeDelta.x * rotationSpeed * Time.deltaTime;
                        float rotationAmountX = swipeDelta.y * rotationSpeed * Time.deltaTime;

                        transform.Rotate(-rotationAmountX, -rotationAmountY, 0);

                        // Обновляем начальную позицию
                        _startTouchPosition = _currentTouchPosition;
                    }
                    break;

                // Завершение касания
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwiping = false;
                    break;
            }
        }
    }
}
