using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Scripts.UI;

namespace Scripts.Services
{
    public class ConfirmPanelService : IConfirmPanelService
    {
        private Transform _confirmationPanel;
        private Button _confirmButton;
        private Button _cancelButton;

        public UnityEvent OnConfirmed { get; set; }

        public ConfirmPanelService(ConfirmPanelElements confirmationPanelElements)
        {
            _confirmationPanel = confirmationPanelElements.confirmationPanel;
            _confirmButton = confirmationPanelElements.confirmButton;
            _cancelButton = confirmationPanelElements.cancelButton;

            Initialize();
        }
        public void Initialize()
        {
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

}