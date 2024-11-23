using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public class TutorialService : ITutorialService
    {      
        private readonly Color _disabledColor = new Color(0.75f, 0.75f,0.75f,1f);
        private readonly ISaveloadDataService _dataManager;
        private readonly Button[] _menuButtons;

        public TutorialService(ISaveloadDataService dataManager, Button[] menuButtons)
        {
            _dataManager = dataManager;
            _menuButtons = menuButtons;  
        }
        public void CheckWorkspaces()
        {
            if (_dataManager.GetItemsCount(MainMenuTypes.Workspaces) == 0)
            {
                for (int i = 1; i < _menuButtons.Length; i++)
                {
                    DeactivateButton(_menuButtons[i]);
                }
            }
            else
            {
                for (int i = 1; i < _menuButtons.Length; i++)
                {
                    ActivateButton(_menuButtons[i]);
                }
            }
        }
        private void ActivateButton(Button button)
        {
            button.image.color = Color.white;
            button.interactable = true;
        }
        private void DeactivateButton(Button button)
        {
            button.image.color = _disabledColor;
            button.interactable = false;
        }
    }
}