using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstTimeAnimatorController : MonoBehaviour
{
    public void LoadSinglePlayer()
    {
        Fade.OnPlay = () => {
            Config.data.firstSinglePlayer = false;
            SceneManager.LoadScene(2);
        };
        Fade.PlayFade();
    }

    public void PlaySound(AudioClip clip)
    {
        Audio.PlaySFX(clip, 1.4f);
    }
}
