using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Markers
{
    [System.Serializable]
    public class ConfirmPanelElements : MonoBehaviour 
    {
        public Transform confirmationPanel;
        public Text messageText;
        public Button confirmButton;
        public Button cancelButton;
    }
}
