using Scripts.Services;
using Scripts.UI.Markers;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IElementsProvider : IService
    {
        GlobalUIElements GlobalUIElements { get; }
        MainMenu MainMenu { get; }
        PopupElements PopupElements { get; }
        ChoiceOfCategoryElements ChoiceOfCategoryElements { get; }
        ItemsAddationViewElements ItemsAddationViewElements { get; }
        StatisticViewElements StatisticViewElements { get; }
        StatisticsInputElements StatisticsInputElements { get; }
        GraphPlane GraphPlane { get; }

    }
}