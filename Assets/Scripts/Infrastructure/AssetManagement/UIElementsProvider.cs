using Scripts.UI.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public class UIElementsProvider : MonoBehaviour, IUIElementsProvider
    {        

        [SerializeField] private Button[] _mainMenuButtons = new Button[10];
        [SerializeField] private GlobalUIElements _globalUIElements;
        [SerializeField] private PopupMessageElements _popupMessageElements;
        [SerializeField] private ConfirmPanelElements _confirmationPanelElements;
        [SerializeField] private ChoiceOfCategoryElements _choiceOfCategoryElements;
        [SerializeField] private ItemsAddationViewElements _itemsAddationViewElements;
        [SerializeField] private StatisticViewElements _statisticViewElements;
        [SerializeField] private StatisticsInputElements _statisticsInputElements;

        public Button[] MainMenuButtons => _mainMenuButtons;
        public GlobalUIElements GlobalUIElements => _globalUIElements;
        public PopupMessageElements PopupMessageElements => _popupMessageElements;
        public ConfirmPanelElements ConfirmationPanelElements => _confirmationPanelElements;

        public ChoiceOfCategoryElements ChoiceOfCategoryElements => _choiceOfCategoryElements;
        public ItemsAddationViewElements ItemsAddationViewElements => _itemsAddationViewElements;
        public StatisticViewElements StatisticViewElements => _statisticViewElements;
        public StatisticsInputElements StatisticsInputElements => _statisticsInputElements;
    }
    [System.Serializable]
    public class GlobalUIElements    
    {
        public Button backButton;
        public Button addationButton;
    }       
   
    
}