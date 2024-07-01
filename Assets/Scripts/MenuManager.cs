using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform _addationMenu;
    [SerializeField] private Transform _listsMenu;
    [SerializeField] private Transform _mainMenu;


    private void Start()
    {
        _addationMenu.gameObject.SetActive(false);
    }
    public void CloseAddationMenu()
    {
        _addationMenu.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
    }
    public void CloseListsMenu()
    {
        _listsMenu.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
    }
    public void OpenAddationMenu()
    {
        _addationMenu.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(false);
    }
    public void OpenListsMenu()
    {        
        _mainMenu.gameObject.SetActive(false);
        _listsMenu.gameObject.SetActive(true);
    }
}
