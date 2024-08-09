using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationPanel : MonoBehaviour
{
    public static ConfirmationPanel instance;
    [SerializeField] private Transform _confirmationPanel;    
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    public  UnityEvent OnConfirmed;

    private void Awake()
    {
        if (instance == null) instance = this;

        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        _confirmationPanel.gameObject.SetActive(false); // Initially hidden
    }

    public void Show()
    {
        _confirmationPanel.gameObject.SetActive(true);
    }

    private void OnConfirmButtonClicked()
    {
        OnConfirmed?.Invoke();
        _confirmationPanel.gameObject.SetActive(false);
    }

    private void OnCancelButtonClicked()
    {
        _confirmationPanel.gameObject.SetActive(false);
    }
}

