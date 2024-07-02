using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform _addationMenu;
    [SerializeField] private Transform _listsMenu;
    [SerializeField] private Transform _mainMenu;
    [SerializeField] private Transform _addationPanel;
    [SerializeField] private Transform _tableView;

    private GameObject[] _menuStack = new GameObject[4];
    private int _menuIndex = 0;

    private void Awake()
    {
        _menuStack[0] = _mainMenu.gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }
    private void Back()
    {       
        if (_menuIndex == 0) return;

        _menuStack[_menuIndex].SetActive(false);
        _menuIndex--;
        _menuStack[_menuIndex].SetActive(true);        
    }
    private void Forwards(GameObject gameObject)
    {
        _menuStack[_menuIndex].SetActive(false);
        _menuIndex++;
        gameObject.SetActive(true);
        _menuStack[_menuIndex] = gameObject;       
    }      
    public void OpenAddationMenu()
    {     
        Forwards(_addationMenu.gameObject);
    }
    public void OpenListsMenu()
    {
        Forwards(_listsMenu.gameObject);
    }
    public void OpenAddationPanel()
    {
        Forwards(_addationPanel.gameObject);
    }
    public void OpenTableView()
    {
        Forwards(_tableView.gameObject);
    }

}
