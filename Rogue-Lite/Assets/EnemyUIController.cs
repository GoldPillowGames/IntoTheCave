using UnityEngine;
using UnityEngine.UI;
using GoldPillowGames.Enemy;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Image _healthbar;
    [SerializeField] private Image _healthbarFollower;

    private float _maxHealth;
    private EnemyController _enemyController;
    private RectTransform _canvas;
    private float _currentHealth;
    private bool _updateHealthFollower;
    private float _timeToUpdateHealth;
    private float _maxTimeToUpdateHealth = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<RectTransform>();
        _enemyController = GetComponentInParent<EnemyController>();
        _maxHealth = _enemyController.Health;
        _currentHealth = _enemyController.Health / _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _healthbar.fillAmount = _enemyController.Health / _maxHealth;
        
        if(_healthbar.fillAmount != _currentHealth)
        {
            _currentHealth = _healthbar.fillAmount;
            _timeToUpdateHealth = _maxTimeToUpdateHealth;
        }

        if(_timeToUpdateHealth > 0)
        {
            _updateHealthFollower = false;
            _timeToUpdateHealth -= Time.deltaTime;
        }
        else
        {
            _updateHealthFollower = true;
        }

        if(_updateHealthFollower)
            _healthbarFollower.fillAmount = Mathf.Lerp(_healthbarFollower.fillAmount, _healthbar.fillAmount, 10 * Time.deltaTime);
    
        if(_healthbar.fillAmount <= 0 && _updateHealthFollower && _healthbarFollower.fillAmount <= _healthbar.fillAmount + 0.15f)
        {
            _canvas.localScale = Vector3.Lerp(_canvas.localScale, Vector3.zero, 10 * Time.deltaTime);
        }
    }
}
