using Scripts.Services;
using Scripts.UI;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IUIElementsProvider : IService
    {      
        Button[] GetMainMenuButtons();
        ConfirmPanelElements GetConfirmationPanelElements();
        PopupMessageElements GetPopupMessageElements();
        ChoiceOfCategoryElements GetChoiceOfCategoryElements();
        GlobalUIElements GetGlobalUIElements();
        ItemsAddationViewElements GetItemsAddationViewElements();
    }
}