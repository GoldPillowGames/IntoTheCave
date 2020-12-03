using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Image originalMousePos;
    public Image currentMousePos;

    Color mouseColor;

    // Start is called before the first frame update
    void Start()
    {
        originalMousePos.color = new Color(1, 1, 1, 0);
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
        originalMousePos.color = Color.Lerp(originalMousePos.color, mouseColor, 20 * Time.deltaTime);
        currentMousePos.color = Color.Lerp(originalMousePos.color, mouseColor, 20 * Time.deltaTime);
    }
}
