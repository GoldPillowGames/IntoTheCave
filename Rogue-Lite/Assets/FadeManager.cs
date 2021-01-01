using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeType
{
    CASUAL,
    UPPER_LOWER,
    LEFT,
    RIGHT,
    INSTANT
}

public class FadeManager : MonoBehaviour
{
    public Animator anim;
    public GameObject loader;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayFade(FadeType fadeType)
    {
        switch (fadeType)
        {
            case FadeType.CASUAL:
                loader.SetActive(false);
                anim.SetTrigger("PlayFade");
                break;
            case FadeType.UPPER_LOWER:
                loader.SetActive(true);
                anim.SetTrigger("PlayFadeUPDOWN");
                break;
            case FadeType.INSTANT:
                loader.SetActive(false);
                anim.SetTrigger("PlayFadeInstant");
                break;
            default:
                loader.SetActive(false);
                anim.SetTrigger("PlayFade");
                break;
        }
        
    }

    public void PlayFade()
    {
        PlayFade(FadeType.CASUAL);
    }

    public void PlayCustomFunction()
    {
        print("Fade");
        Fade.OnPlay();
    }
}
