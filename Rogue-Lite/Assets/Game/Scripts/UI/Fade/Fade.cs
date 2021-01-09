using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Fade
{
    public delegate void Play();
    public static Play OnPlay;
    public static FadeManager fade;

    public static void PlayFade(FadeType fadeType)
    {
        fade = GameObject.FindObjectOfType<FadeManager>();

        if (fade)
        {
            fade.PlayFade(fadeType);
        }
        else
        {
            GameObject fadeObject = Resources.Load<GameObject>("Prefabs/Fade");
            fadeObject.transform.parent = null;
            fadeObject.transform.position = Vector3.zero;
            GameObject newFade = MonoBehaviour.Instantiate<GameObject>(fadeObject);
            newFade.GetComponent<FadeManager>().PlayFade(fadeType);
        }
    }

    public static void SetTimeEffect(bool timeEffect)
    {
        fade = GameObject.FindObjectOfType<FadeManager>();

        if (fade)
        {
            fade.SetTimeEffect(timeEffect);
        }
        else
        {
            GameObject fadeObject = Resources.Load<GameObject>("Prefabs/Fade");
            fadeObject.transform.parent = null;
            fadeObject.transform.position = Vector3.zero;
            GameObject newFade = MonoBehaviour.Instantiate<GameObject>(fadeObject);
            newFade.GetComponent<FadeManager>().SetTimeEffect(timeEffect);
        }
    }

    public static void PlayFade()
    {
        PlayFade(FadeType.CASUAL);
    }
}
