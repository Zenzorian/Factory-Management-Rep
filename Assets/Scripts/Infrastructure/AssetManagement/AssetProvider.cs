using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : MonoBehaviour, IAssetProvider
    {
        [SerializeField] private Button[] _mainMenuButtons = new Button[10];
        [SerializeField] private PopupMessageElements _popupMessageElements;
        [SerializeField] private ConfirmPanelElements _confirmationPanelElements;

        public Button[] GetMainMenuButtons() => _mainMenuButtons;
        public PopupMessageElements GetPopupMessageElements() => _popupMessageElements;
        public ConfirmPanelElements GetConfirmationPanelElements() => _confirmationPanelElements;
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
}