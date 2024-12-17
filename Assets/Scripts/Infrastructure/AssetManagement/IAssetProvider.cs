using Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject GetButtonPrefab();
        Button[] GetMainMenuButtons();
        ConfirmPanelElements GetConfirmationPanelElements();
        PopupMessageElements GetPopupMessageElements();
        ChoiceOfCategoryElements GetChoiceOfCategoryElements();
        GlobalUIElements GetGlobalUIElements();
        ItemsAddationViewElements GetItemsAddationViewElements();
    }
}