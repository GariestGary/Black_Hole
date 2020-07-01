using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.InternalUtil;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private bool Player = false;

	public MovementComponent Movement { get; private set; }
	public CharacterAnimation Animation { get; private set; }

	private IAbility _ability;
	private bool _casting;
	private bool _stunned;
	private bool _abilityInQueue;
	private Vector3 _lastClickedPosition;
	private IDisposable _distanceCheck;
		 
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

	public void SetStun(bool state)
	{
		_stunned = state;
		Animation.SetStunned(state);
	}

	public void SetCast(bool state)
	{
		if (_stunned)
		{
			return;
		}

		_casting = state;
		Animation.SetCasting(state);

		if (!state)
		{
			if (_ability != null)
			{
				_ability.Break();
			}
		}
	}

	public void LeftClick()
	{
		_lastClickedPosition = MainCamera.Instance.MousePositionInWorldSpace;

		if (CursorHandler.Instance.isOverUI || _casting || _stunned)
		{
			return;
		}

		if(_abilityInQueue)
		{
			_abilityInQueue = false;
			DisposeCheck(_distanceCheck);
		}

		if (_ability != null)
		{
			if (IsPlayerInAbilityRadius())
			{
				Movement.Stop();
				_ability.UseAbility(_lastClickedPosition);
				_ability = null;
				CursorHandler.Instance.SetDefault();
				return;
			}
			else
			{
				Movement.MoveToCursorPosition();
				
				_abilityInQueue = true;

				_distanceCheck = Observable.EveryUpdate().Where(_ => IsPlayerInAbilityRadius()).Subscribe(_ => 
				{
					LeftClick();
				});
			}

		}

		Movement.MoveToCursorPosition();
	}

	private void DisposeCheck(IDisposable d)
	{
		if (d == null)
		{
			return;
		}
		else
		{
			d.Dispose();
		}
	}

	public void RightClick()
	{
		if (_stunned)
		{
			return;
		}

		CursorHandler.Instance.SetDefault();
		_ability = null;
		_abilityInQueue = false;
		DisposeCheck(_distanceCheck);
	}

	private bool IsPlayerInAbilityRadius()
	{
		if (_ability != null)
		{
			float dist = Vector3.Distance(transform.position, MainCamera.Instance.MousePositionInWorldSpace);

			if (dist > _ability.ActivationRadius)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		return false;
	}
}
