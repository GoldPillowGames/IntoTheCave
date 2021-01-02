using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageTextController : MonoBehaviour
{
    public void Init(int damage)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        GetComponentInChildren<TextMeshProUGUI>().rectTransform.rotation = Quaternion.Euler(0,0, Random.Range(-20, 21));
        GetComponent<Animator>().SetTrigger("Start");
    }
}
