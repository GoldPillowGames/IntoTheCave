using System.Collections;
using System.Collections.Generic;
using GoldPillowGames.Enemy;
using UnityEngine;

namespace GoldPillowGames.Trap
{
    public class TrapController : MonoBehaviour
    {
    
        #region Variables
        [SerializeField] private int damage = 10;
        [SerializeField] private float timeToComeUp;
        [SerializeField] private float timeUp;
        [SerializeField] private float timeToDamageAgain;
        [SerializeField] private float timeToBeReadyToComeUp;
        private bool _isReadyToCome = true;
        private bool _isOn;
        private bool _isPreparingToComeUp;
        private readonly List<GameObject> _entitiesHit = new List<GameObject>();
        
        private Animator _animator;
        
        private static readonly int On = Animator.StringToHash("IsOn");
        #endregion

        #region Methods
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            //if (!Config.data.isOnline)
            //    damage *= Config.data.dungeonLevel;
        }

        private void OnTriggerStay(Collider other)
        {
            var colliderTag = other.tag;
            var isEntity = (colliderTag == "Player") || (colliderTag == "Enemy");

            if (!isEntity)
            {
                return;
            }
            
            if (!_isOn)
            {
                if (!_isReadyToCome)
                {
                    return;
                }
                
                if (!_isPreparingToComeUp)
                {
                    PrepareToComeUp();
                }
            }
            else
            {
                if (_entitiesHit.Contains(other.gameObject))
                {
                    return;
                }
                
                switch (colliderTag)
                {
                    case "Player":
                        other.gameObject.GetComponent<PlayerController>().TakeDamage(damage,
                            (other.transform.position - transform.position).normalized);
                        break;
                    case "Enemy":
                    {
                        var enemyController = other.gameObject.GetComponent<EnemyController>();
                        enemyController.ReceiveDamage(damage);
                        enemyController.Push(0.5f, 4, (other.transform.position - transform.position).normalized);
                        break;
                    }
                }
            
                _entitiesHit.Add(other.gameObject);
                StartCoroutine(RemoveFromEntitiesList(other.gameObject));
            }
        }

        private void PrepareToComeUp()
        {
            _isPreparingToComeUp = true;
            CameraShaker.Shake(timeToComeUp, 0.3f, 1);
            Invoke(nameof(ComeUp), timeToComeUp);
        }

        private void WaitUntilLeaving()
        {
            Invoke(nameof(Hide), timeUp);
        }

        private void Hide()
        {
            _animator.SetBool(On, false);
        }

        private void ComeUp()
        {
            _animator.SetBool(On, true);
        }

        private void SetOn()
        {
            _isOn = true;
        }

        private void SetOff()
        {
            _isOn = false;
            _isReadyToCome = false;
            _isPreparingToComeUp = false;
            _entitiesHit.Clear();
            StartCoroutine(ReadyToCome());
        }

        private IEnumerator ReadyToCome()
        {
            yield return new WaitForSeconds(timeToBeReadyToComeUp);
            
            _isReadyToCome = true;
        }
        
        private IEnumerator RemoveFromEntitiesList(GameObject entity)
        {
            yield return new WaitForSeconds(timeToDamageAgain);

            if (_isOn)
            {
                _entitiesHit.Remove(entity);
            }
        }
        #endregion
    
    }
}
