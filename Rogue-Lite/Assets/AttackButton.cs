using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : Button
{
    [SerializeField] private PlayerController _player;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (!_player)
            _player = GetComponentInParent<UIController>().player;
        _player.TactileAttack();
        Debug.Log("Down");
        //show text
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (!_player)
            _player = GetComponentInParent<UIController>().player;
        // _player.isTactileAttacking = false;
        _player.TactileAttack();
        Debug.Log("Up");
        //hide text
    }
}
