using System.Collections;
using System.Collections.Generic;
using FactoryManager;
using Unity.Android.Gradle.Manifest;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private Button[] _menuButtons;
    [SerializeField] private Color _disabledColor;

    private void OnEnable()
    {
        CheckWorkstations();
    }
    private void CheckWorkstations()
    {
        if(_dataManager.GetItemsCount(MainMenuTypes.Workstations)==0)
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
        button.interactable= true;
    }
    private void DeactivateButton(Button button)
    {
        button.image.color = _disabledColor;
        button.interactable= false;
    }
}
