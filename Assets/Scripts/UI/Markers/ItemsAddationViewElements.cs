using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Markers
{
    [System.Serializable]
    public class ItemsAddationViewElements : MonoBehaviour
    {
        public Transform addationPanel;
        public Transform content;
        public InputFieldCreator inputFieldCreator = new InputFieldCreator();
        public Button addButton;
        public Button closeButton;

        public StatisticAddationViewElements statisticAddationViewElements;
    }
    [System.Serializable]
    public class StatisticAddationViewElements
    {
        public Dropdown dropdown;
        public Button button;
        public Text lableText;
    }
}
