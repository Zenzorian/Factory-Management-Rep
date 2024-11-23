using UnityEngine.Events;

namespace Scripts.Services
{
    public interface IConfirmPanelService : IService
    {
        UnityEvent OnConfirmed { get; set; }
        void Show();
    }
}
