using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UIPopupMessage : MonoBehaviour
{
    public static UIPopupMessage instance;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private Text _messageText;
    [SerializeField] private float _messageDuration = 3.0f;

    private Task _messageTask;

    private void Awake()
    {
        if(instance == null)instance = this;
        
        _messagePanel.SetActive(false);
    }

    public async void ShowMessage(string message)
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

        await Task.Delay((int)(_messageDuration * 1000));

        _messagePanel.SetActive(false);
    }

    private void StopMessage()
    {
        _messagePanel.SetActive(false);
        _messageTask = null;
    }
}
