using Scripts.UI.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public class ElementsProvider : MonoBehaviour, IUIElementsProvider
    {        

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GlobalUIElements _globalUIElements;
        [SerializeField] private PopupMessageElements _popupMessageElements;
        [SerializeField] private ConfirmPanelElements _confirmationPanelElements;
        [SerializeField] private ChoiceOfCategoryElements _choiceOfCategoryElements;
        [SerializeField] private ItemsAddationViewElements _itemsAddationViewElements;
        [SerializeField] private StatisticViewElements _statisticViewElements;
        [SerializeField] private StatisticsInputElements _statisticsInputElements;
        [SerializeField] private GraphPlane _graphPlane;

        public MainMenu MainMenu => _mainMenu;
        public GlobalUIElements GlobalUIElements => _globalUIElements;
        public PopupMessageElements PopupMessageElements => _popupMessageElements;
        public ConfirmPanelElements ConfirmationPanelElements => _confirmationPanelElements;

        public ChoiceOfCategoryElements ChoiceOfCategoryElements => _choiceOfCategoryElements;
        public ItemsAddationViewElements ItemsAddationViewElements => _itemsAddationViewElements;
        public StatisticViewElements StatisticViewElements => _statisticViewElements;
        public StatisticsInputElements StatisticsInputElements => _statisticsInputElements;
        public GraphPlane GraphPlane => _graphPlane;
    }
    [System.Serializable]
    public class GlobalUIElements    
    {
        public Button backButton;
        public Button addationButton;
    }       
   
    
}