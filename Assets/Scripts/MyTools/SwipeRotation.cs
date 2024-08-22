using UnityEngine;

public class SwipeRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f; // �������� �������� �������

    private Vector2 _startTouchPosition;
    private Vector2 _currentTouchPosition;
    private bool _isSwiping;

    void Update()
    {
        // ���������, ���� ������� �� ������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                // ������ �������
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    _isSwiping = true;
                    break;

                // ����������� ������ �� ������
                case TouchPhase.Moved:
                    if (_isSwiping)
                    {
                        _currentTouchPosition = touch.position;

                        // ������� � ��������� �������
                        Vector2 swipeDelta = _currentTouchPosition - _startTouchPosition;

                        // ������� ������ �� ���� Y � X, ���������� �� �������������� � ������������ �������
                        float rotationAmountY = swipeDelta.x * rotationSpeed * Time.deltaTime;
                        float rotationAmountX = swipeDelta.y * rotationSpeed * Time.deltaTime;

                        transform.Rotate(-rotationAmountX, -rotationAmountY, 0);

                        // ��������� ��������� �������
                        _startTouchPosition = _currentTouchPosition;
                    }
                    break;

                // ���������� �������
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwiping = false;
                    break;
            }
        }
    }
}
