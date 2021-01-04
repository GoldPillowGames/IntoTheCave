using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public void SetMenu(int menu)
    {
        GetComponentInParent<MenuManager>().ShowMenu(menu);
    }
}
