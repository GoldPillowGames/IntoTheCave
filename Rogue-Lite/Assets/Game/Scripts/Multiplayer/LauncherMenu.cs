using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherMenu : MonoBehaviour
{
    public string menuName;
    public bool open = false;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
