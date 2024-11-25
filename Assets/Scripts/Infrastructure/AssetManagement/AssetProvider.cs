using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : MonoBehaviour, IAssetProvider
    {
        [SerializeField] private GameObject _buttonPrefab;

        [SerializeField] private Button[] _mainMenuButtons = new Button[10];
        [SerializeField] private GlobalUIElements _globalUIElements;
        [SerializeField] private PopupMessageElements _popupMessageElements;
        [SerializeField] private ConfirmPanelElements _confirmationPanelElements;
        [SerializeField] private ChoiceOfCategoryElements _choiceOfCategoryElements;

        public GameObject GetButtonPrefab() => _buttonPrefab;
        public Button[] GetMainMenuButtons() => _mainMenuButtons;
        public GlobalUIElements GetGlobalUIElements() => _globalUIElements;
        public PopupMessageElements GetPopupMessageElements() => _popupMessageElements;
        public ConfirmPanelElements GetConfirmationPanelElements() => _confirmationPanelElements;

        public ChoiceOfCategoryElements GetChoiceOfCategoryElements() => _choiceOfCategoryElements;
    }
    [System.Serializable]
    public class GlobalUIElements    
    {
        public Button backButton;
        public Button addationButton;
    }
    [System.Serializable]
    public class PopupMessageElements
    {
        public GameObject MassagePanel;
        public Text MassageText;
    }
    [System.Serializable]
    public class ConfirmPanelElements
    {
        public Transform confirmationPanel;
        public Button confirmButton;
        public Button cancelButton;
    }
    [System.Serializable]
    public class ChoiceOfCategoryElements
    {
        public Transform panel;
        public Text sectionNameText;       
        public Transform content;
    }
}