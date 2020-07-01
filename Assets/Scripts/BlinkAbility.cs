using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAbility : MonoBehaviour, IAbility
{
	[SerializeField] private float _radius;
	[SerializeField] private float _activationRadius;
	[SerializeField] private GameObject _blinkEffect;

	private GameObject _startEffect;
	private GameObject _endEffect;

	public float Radius => _radius;

	public float ActivationRadius => _activationRadius;

	public void Break()
	{
		GameManager.Instance.Player.SetAbility(null);
		CursorHandler.Instance.SetDefault();
	}

	public void OnAbilitySelected()
	{
		GameManager.Instance.Player.SetAbility(this);
		CursorHandler.Instance.SetArrow();
	}

	public void UseAbility(Vector3 position)
	{
		_startEffect = Instantiate(_blinkEffect, GameManager.Instance.Player.transform.position, Quaternion.identity);
		_endEffect = Instantiate(_blinkEffect, position, Quaternion.identity);

		Vector3 dir = position - GameManager.Instance.Player.transform.position;

		GameManager.Instance.Player.transform.position = GameManager.Instance.Player.transform.position + dir;
		GameManager.Instance.Player.Movement.Move(position);


		StartCoroutine(DestroyDelay());
	}

	private IEnumerator DestroyDelay()
	{
		yield return new WaitForSeconds(2);

		Destroy(_startEffect);
		Destroy(_endEffect);
	}
}
