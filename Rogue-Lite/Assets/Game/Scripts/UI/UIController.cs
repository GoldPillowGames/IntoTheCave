using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Photon.Pun;
public class UIController : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private Image _playerHealthbar;
    [SerializeField] private Image _playerHealthbarWhiteBackground;
    [SerializeField] private DeathMenuManager _deathMenuManager;
    [SerializeField] private GameObject _leftJoystick;
    [SerializeField] private GameObject _rightJoystick;
    [SerializeField] private GameObject _rollButton;
    [SerializeField] private Button _attackButton;

    [HideInInspector] public bool _playerIsDead = false;

    private void Awake()
    {
        if (Config.data.isOnline)
        {
            if (!GetComponentInParent<PhotonView>().IsMine)
            {
                Destroy(this.gameObject);
            }
        }
        

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _leftJoystick.SetActive(Config.data.isTactile ? true : false);
        // _rightJoystick.GetComponent<Joystick>().enabled = Config.data.isTactile ? true : false;
        // _rightJoystick.SetActive(Config.data.isTactile ? true : false);
        _rollButton.SetActive(Config.data.isTactile ? true : false);
        _attackButton.gameObject.SetActive(Config.data.isTactile ? true : false);

        CanvasScaler canvasScale = GetComponent<CanvasScaler>();
        canvasScale.referenceResolution = new Vector2(
            canvasScale.referenceResolution.x / (Config.data.canvasScale), 
            canvasScale.referenceResolution.y / (Config.data.canvasScale)
            );
    }

    // Update is called once per frame
    void Update()
    {
        _playerHealthbar.fillAmount = (float)player.health / (float)player.maxHealth;
        _playerHealthbarWhiteBackground.fillAmount = Mathf.Lerp(_playerHealthbarWhiteBackground.fillAmount, _playerHealthbar.fillAmount, 10 * Time.deltaTime);

        if(!_playerIsDead && player.health <= 0)
        {
            _playerIsDead = true;
            player.Kill();
            if(!Config.data.isOnline)
                _deathMenuManager.PlayDeathMenu();
        }
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    _player.isTactileAttacking = true;
    //}
}
