using Scripts.UI;
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
       
        public Button[] GetMainMenuButtons() => _mainMenuButtons;
        public GlobalUIElements GetGlobalUIElements() => _globalUIElements;
        public PopupMessageElements GetPopupMessageElements() => _popupMessageElements;
        public ConfirmPanelElements GetConfirmationPanelElements() => _confirmationPanelElements;

        public ChoiceOfCategoryElements GetChoiceOfCategoryElements() => _choiceOfCategoryElements;
        public ItemsAddationViewElements GetItemsAddationViewElements() => _itemsAddationViewElements;
    }
    [System.Serializable]
    public class GlobalUIElements    
    {
        public Button backButton;
        public Button addationButton;
    }       
   
    
}