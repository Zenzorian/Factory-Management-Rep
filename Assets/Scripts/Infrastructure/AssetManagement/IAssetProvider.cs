using Scripts.Services;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Button[] GetMainMenuButtons();
        ConfirmPanelElements GetConfirmationPanelElements();
        PopupMessageElements GetPopupMessageElements();
    }
}