﻿using UnityEngine;
using UnityEngine.UI;
using Scripts.UI.Markers;
using System;

namespace Scripts.Services
{
    public class ConfirmPanelService : IConfirmPanelService
    {
        private Transform _confirmationPanel;
        private Button _confirmButton;
        private Button _cancelButton;

        public Action _onConfirmed;

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

        public void Show(Action onConfirmed)
        {
            _confirmationPanel.gameObject.SetActive(true);
            _onConfirmed = onConfirmed;
        }

        private void OnConfirmButtonClicked()
        {
            _onConfirmed?.Invoke();
            _confirmationPanel.gameObject.SetActive(false);
        }

        private void OnCancelButtonClicked()
        {
            _confirmationPanel.gameObject.SetActive(false);
        }
    }

}