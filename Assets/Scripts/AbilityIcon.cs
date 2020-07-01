﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AbilityIcon : MonoBehaviour, IPointerClickHandler
{
	private IAbility _ability;

	private void Awake()
	{
		_ability = GetComponent<IAbility>();
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		GameManager.Instance.Player.SetAbility(_ability);
		CursorHandler.Instance.SetCircle(_ability.Radius);
	}
}
