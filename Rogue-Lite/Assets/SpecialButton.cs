using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpecialButton : Button
{
    [SerializeField] private PlayerController _player;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (!_player)
            _player = GetComponentInParent<UIController>().player;

        _player.SpecialSkillDown();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (!_player)
            _player = GetComponentInParent<UIController>().player;

        _player.SpecialSkillUp();
    }
}
