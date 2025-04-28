using System;

namespace Scripts.Services
{
    public interface IPopUpService : IService
    {
        void ShowConfirm(string message, Action onConfirmed);
        void ShowMessage(string message, MessageType messageType);
        void ShowMessageAutoClose(string message, MessageType messageType);
        void CloseMessage();
    }
}
