using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [System.Serializable]
    public class ItemsAddationViewElements : MonoBehaviour
    {
        public Transform addationPanel;
        public Transform content;
        public InputFieldCreator inputFieldCreator = new InputFieldCreator();
        public Button addButton;
        public Button closeButton;
    }
}
