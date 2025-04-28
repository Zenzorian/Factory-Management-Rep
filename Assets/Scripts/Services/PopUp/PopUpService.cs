using UnityEngine;
using Scripts.UI.Markers;
using System.Collections;
using Scripts.Infrastructure;
using System;

namespace Scripts.Services
{
    public class PopUpService : IPopUpService
    {
        public Action _onConfirmed;

        private readonly ICoroutineRunner _сoroutineRunner;

        private GameObject _background;
        private Confirm _confirm;
        private Message _message;

        private const float MessageDuration = 3f;       
        private Coroutine _messageCoroutine;
        
        private Color[] _colors = new Color[]
        {
            Color.black,
            Color.yellow,
            Color.red
        };

        public PopUpService(PopupElements popupElements, ICoroutineRunner сoroutineRunner)
        {
            _сoroutineRunner = сoroutineRunner;
            _background = popupElements.background;
            _confirm = popupElements.confirm;
            _message = popupElements.message;

            Initialize();
        }
        public void Initialize()
        {
            _confirm.confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            _confirm.cancelButton.onClick.AddListener(OnCancelButtonClicked);
            _confirm.panel.gameObject.SetActive(false);
        }

        public void ShowConfirm(string message, Action onConfirmed)
        {
            _message.messageText.text = message;
            _confirm.panel.gameObject.SetActive(true);
            _onConfirmed = onConfirmed;
            _background.gameObject.SetActive(true);
        }

        private void OnConfirmButtonClicked()
        {
            _onConfirmed?.Invoke();
            _confirm.panel.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
        }

        private void OnCancelButtonClicked()
        {
            _confirm.panel.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
        }
        
        public void ShowMessage(string message, MessageType messageType)
        {
            _message.messageText.text = message;
            _message.messageText.color = _colors[(int)messageType];
            _message.panel.SetActive(true);
            _background.gameObject.SetActive(true);
        }

        public void ShowMessageAutoClose(string message, MessageType messageType)
        {
            if (_messageCoroutine != null)
            {
                _сoroutineRunner.StopCoroutine(_messageCoroutine);
            }
            _messageCoroutine = _сoroutineRunner.StartCoroutine(DisplayMessageCoroutine(message, messageType));
        }

        private IEnumerator DisplayMessageCoroutine(string message, MessageType messageType)
        {
            _message.messageText.text = message;
            _message.messageText.color = _colors[(int)messageType];
            _message.panel.SetActive(true);
            _background.gameObject.SetActive(true);
            yield return new WaitForSeconds(MessageDuration);

            _message.panel.SetActive(false);
            _background.gameObject.SetActive(false);
            _messageCoroutine = null;
        }

        public void CloseMessage()
        {
            _message.panel.SetActive(false);
            _background.gameObject.SetActive(false);           
        }
    }

    public enum MessageType
    {
        message,
        warning,
        error
    }
}