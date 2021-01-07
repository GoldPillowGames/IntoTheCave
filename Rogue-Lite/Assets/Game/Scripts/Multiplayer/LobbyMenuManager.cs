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
        //if(menuName != "loading" && menuName != "room")
        //{
        //    Fade.OnPlay = () =>
        //    {
                
        //    };
        //    Fade.PlayFade();
        //}
        //else
        //{
        //    for (int i = 0; i < _menus.Length; i++)
        //    {
        //        if (_menus[i].menuName == menuName)
        //            OpenMenu(_menus[i]);
        //        else if (_menus[i].open)
        //            CloseMenu(_menus[i]);
        //    }
        //}

        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].menuName == menuName)
                OpenMenu(_menus[i]);
            else if (_menus[i].open)
                CloseMenu(_menus[i]);
        }

    }

    public void OpenMenu(LauncherMenu menu)
    {
        
        //Fade.OnPlay = () =>
        //{
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].open)
                CloseMenu(_menus[i]);
        }
        menu.Open();
        //};
        //Fade.PlayFade();
    }

    public void ReturnToMainMenu()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
        Fade.OnPlay = () =>
        {
            Destroy(FindObjectOfType<OnlineRoomManager>().gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        };
        Fade.PlayFade();
    }

    public void CloseMenu(LauncherMenu menu)
    {
        menu.Close();
    }
}
