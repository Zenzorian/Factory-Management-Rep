using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [System.Serializable]
    public class ChoiceOfCategoryElements : MonoBehaviour
    {
        public Transform panel;
        public Text sectionNameText;
        public Transform content;

        public GameObject choiceButtonPrefab;
    }
}
