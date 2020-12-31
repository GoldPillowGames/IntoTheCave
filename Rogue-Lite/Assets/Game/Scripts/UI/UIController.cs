using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _playerHealthbar;
    [SerializeField] private Image _playerHealthbarWhiteBackground;
    [SerializeField] private DeathMenuManager _deathMenuManager;
    [SerializeField] private GameObject _joystick;

    [HideInInspector] public bool _playerIsDead = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _joystick.SetActive(Config.data.isTactile ? true : false);
        CanvasScaler canvasScale = GetComponent<CanvasScaler>();
        canvasScale.referenceResolution = new Vector2(
            canvasScale.referenceResolution.x / (Config.data.canvasScale), 
            canvasScale.referenceResolution.y / (Config.data.canvasScale)
            );
    }

    // Update is called once per frame
    void Update()
    {
        _playerHealthbar.fillAmount = (float)_player.health / (float)_player.maxHealth;
        _playerHealthbarWhiteBackground.fillAmount = Mathf.Lerp(_playerHealthbarWhiteBackground.fillAmount, _playerHealthbar.fillAmount, 10 * Time.deltaTime);

        if(!_playerIsDead && _player.health <= 0)
        {
            _playerIsDead = true;
            _player.Kill();
            _deathMenuManager.PlayDeathMenu();
        }
    }
}
