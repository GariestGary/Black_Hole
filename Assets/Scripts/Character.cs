using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private bool Player = false;

	public MovementComponent Movement { get; private set; }
	public CharacterAnimation Animation { get; private set; }

	private IAbility _ability;
	private bool isOnUI;

	private void Awake()
	{
		Movement = GetComponent<MovementComponent>();
		Animation = GetComponent<CharacterAnimation>();

		if (Player)
		{
			GameManager.Instance.InitializePlayer(this);
		}
	}

	public void SetAbility(IAbility ability)
	{
		_ability = ability;
	}

	public void LeftClick()
	{
		if (isOnUI)
		{
			return;
		}

		if (_ability != null)
		{
			_ability.UseAbility();
			_ability = null;
			return;
		}

		
	}
}
