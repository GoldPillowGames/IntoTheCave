using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenuManager : MonoBehaviour
{
    public static LobbyMenuManager Instance;

    [SerializeField] private LauncherMenu[] _menus;

    private void Awake()
    {
        OpenMenu("loading");
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].menuName == menuName)
                OpenMenu(_menus[i]);
            else if(_menus[i].open)
                CloseMenu(_menus[i]);
        }
    }

    public void OpenMenu(LauncherMenu menu)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].open)
                CloseMenu(_menus[i]);
        }
        menu.Open();
    }

    public void CloseMenu(LauncherMenu menu)
    {
        menu.Close();
    }
}
