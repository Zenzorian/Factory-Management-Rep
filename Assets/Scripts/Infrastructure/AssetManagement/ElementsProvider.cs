using Scripts.UI.Markers;
using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public class ElementsProvider : MonoBehaviour, IElementsProvider
    {        

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GlobalUIElements _globalUIElements;
        [SerializeField] private PopupElements _popupElements;
        [SerializeField] private ChoiceOfCategoryElements _choiceOfCategoryElements;
        [SerializeField] private ItemsAddationViewElements _itemsAddationViewElements;
        [SerializeField] private StatisticViewElements _statisticViewElements;
        [SerializeField] private StatisticsInputElements _statisticsInputElements;
        [SerializeField] private GraphPlane _graphPlane;

        public MainMenu MainMenu => _mainMenu;
        public GlobalUIElements GlobalUIElements => _globalUIElements;
        public PopupElements PopupElements => _popupElements;

        public ChoiceOfCategoryElements ChoiceOfCategoryElements => _choiceOfCategoryElements;
        public ItemsAddationViewElements ItemsAddationViewElements => _itemsAddationViewElements;
        public StatisticViewElements StatisticViewElements => _statisticViewElements;
        public StatisticsInputElements StatisticsInputElements => _statisticsInputElements;
        public GraphPlane GraphPlane => _graphPlane;
    }   
}