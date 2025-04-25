using System;

namespace Scripts.Services
{
    public interface IConfirmPanelService : IService
    {       
        void Show(string message, Action onConfirmed);
    }
}
