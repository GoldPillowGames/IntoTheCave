using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorTexture
{
    IDLE,
    CLICK
}

public class CursorManager : MonoBehaviour
{

    public static CursorManager Instance;
    [SerializeField] private Texture2D idleTexture;

    
    public Sprite idleSprite;
    public Sprite clickSprite;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SetTexture(CursorTexture texture)
    {
        switch (texture)
        {
            case CursorTexture.IDLE:
                Cursor.SetCursor(idleTexture, Vector2.zero, CursorMode.Auto);
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTexture(CursorTexture.IDLE);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
