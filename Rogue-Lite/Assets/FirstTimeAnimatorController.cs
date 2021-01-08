using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstTimeAnimatorController : MonoBehaviour
{
    public void LoadSinglePlayer()
    {
        Fade.OnPlay = () => {
            SceneManager.LoadScene(2);
        };
        Fade.PlayFade();
    }
}
