using Scripts.Services;
using Scripts.UI;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IUIElementsProvider : IService
    {
        Button[] MainMenuButtons { get; }
        ConfirmPanelElements ConfirmationPanelElements { get; }
        PopupMessageElements PopupMessageElements { get; }
        ChoiceOfCategoryElements ChoiceOfCategoryElements { get; }
        GlobalUIElements GlobalUIElements { get; }
        ItemsAddationViewElements ItemsAddationViewElements { get; }
        StatisticViewElements StatisticViewElements { get; }
    }
}