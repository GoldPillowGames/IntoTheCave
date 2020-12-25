using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale = 1;
    [SerializeField] private float _transitionSmoothness = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, timeScale, _transitionSmoothness * Time.unscaledDeltaTime);
    }
}
