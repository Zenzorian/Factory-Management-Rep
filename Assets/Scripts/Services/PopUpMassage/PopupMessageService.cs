using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Scripts.Services;
using Scripts.UI.Markers;

public class PopupMessageService : MonoBehaviour, IPopUpMassageService
{    
    private GameObject _messagePanel;
    private Text _messageText;
    private const float  MessageDuration = 3.0f;

    private Task _messageTask;

    public PopupMessageService(PopupMessageElements popupMessageElements)
    {
        _messagePanel = popupMessageElements.MassagePanel;
        _messageText = popupMessageElements.MassageText;
    }
    public async void Show(string message)
    {        
        if (_messageTask != null && !_messageTask.IsCompleted)
        {
            StopMessage();
        }

        _messageTask = DisplayMessageAsync(message);
        await _messageTask;       
    }

    private async Task DisplayMessageAsync(string message)
    {
        _messageText.text = message;
        _messagePanel.SetActive(true);

        await Task.Delay((int)(MessageDuration * 1000));

        _messagePanel.SetActive(false);
    }

    private void StopMessage()
    {
        _messagePanel.SetActive(false);
        _messageTask = null;
    }
}
