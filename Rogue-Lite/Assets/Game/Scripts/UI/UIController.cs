using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Image originalMousePos;
    public Image currentMousePos;

    public PlayerController player;
    public Image playerHealthbar;
    public Image playerHealthbarWhiteBackground;

    [SerializeField] private DeathMenuManager _deathMenuManager;

    private bool _playerIsDead = false;

    Color mouseColor;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(originalMousePos)
            originalMousePos.color = new Color(1, 1, 1, 0);
        if(currentMousePos)
            currentMousePos.color = new Color(1, 1, 1, 0);
        mouseColor = new Color(1, 1, 1, 0);
    }

    public void SetMousePos(Vector2 originalMousePos, Vector2 currentMousePos)
    {
        this.originalMousePos.rectTransform.position = originalMousePos;
        this.currentMousePos.rectTransform.position = currentMousePos;
    }

    public void ShowInput()
    {
        mouseColor = new Color(1, 1, 1, 1);
    }

    public void HideInput()
    {
        mouseColor = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthbar.fillAmount = (float)player.health / (float)player.maxHealth;
        playerHealthbarWhiteBackground.fillAmount = Mathf.Lerp(playerHealthbarWhiteBackground.fillAmount, playerHealthbar.fillAmount, 10 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.health -= 20;
        }

        if(!_playerIsDead && player.health <= 0)
        {
            _playerIsDead = true;
            _deathMenuManager.PlayDeathMenu();
        }

        

        //originalMousePos.color = Color.Lerp(originalMousePos.color, mouseColor, 20 * Time.deltaTime);
        //currentMousePos.color = Color.Lerp(originalMousePos.color, mouseColor, 20 * Time.deltaTime);
    }
}
