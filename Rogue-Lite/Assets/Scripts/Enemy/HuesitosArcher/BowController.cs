using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class BowController : MonoBehaviour
    {
        [SerializeField] private GameObject archer;
        [SerializeField] private GameObject visualArrow;
        [SerializeField] private GameObject arrowPrefab;
        //private MeshCollider _collider;
        //private Rigidbody _rigidbody;
        
        private void Awake()
        {
            //_collider = GetComponent<MeshCollider>();
            //_rigidbody = GetComponent<Rigidbody>();
            //_collider.enabled = false;
        }

        public void ShowVisualArrow()
        {
            visualArrow.SetActive(true);
        }

        public void HideVisualArrow()
        {
            visualArrow.SetActive(false);
        }
        
        public void ThrowArrow()
        {
            var arrow = ObjectPool.Instance.GetObject(arrowPrefab);
            arrow.GetComponent<ArrowController>().Init(visualArrow.transform.position, archer.transform.forward);
            visualArrow.SetActive(false);
        }
        
        public void Disable()
        {
            //_collider.enabled = true;
            //_rigidbody.isKinematic = false;
        }
    }
}