using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Markers
{
    [System.Serializable]
    public class PopupElements: MonoBehaviour
    { 
        public GameObject background;
        public Message message = new Message();
        public Confirm confirm = new Confirm();
    }
    [System.Serializable]
    public class Message
    { 
        public GameObject panel;
        public Text messageText;
    }
     [System.Serializable]
    public class Confirm
    {
        public GameObject panel;
        public Text messageText;
        public Button confirmButton;
        public Button cancelButton;
    }
}