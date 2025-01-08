using Scripts.Services;
using Scripts.UI.Markers;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IElementsProvider : IService
    {
        GlobalUIElements GlobalUIElements { get; }
        MainMenu MainMenu { get; }
        ConfirmPanelElements ConfirmationPanelElements { get; }
        PopupMessageElements PopupMessageElements { get; }
        ChoiceOfCategoryElements ChoiceOfCategoryElements { get; }
        ItemsAddationViewElements ItemsAddationViewElements { get; }
        StatisticViewElements StatisticViewElements { get; }
        StatisticsInputElements StatisticsInputElements { get; }
        GraphPlane GraphPlane { get; }

    }
}