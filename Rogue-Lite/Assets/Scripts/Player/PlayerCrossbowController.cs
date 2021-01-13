using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Player
{
    public class PlayerCrossbowController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject arrowSpawner;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private AudioClip shootSound;
        
        public void ThrowArrow()
        {
            var arrow = ObjectPool.Instance.GetObject(arrowPrefab);
            arrow.GetComponent<CrossbowArrowController>().Init(arrowSpawner.transform.position, player.transform.forward);
            arrowSpawner.SetActive(false);
            if(shootSound)
                Audio.PlaySFX(shootSound);
        }
    }
}
