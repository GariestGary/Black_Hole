using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA.Input;

public class MovementComponent : MonoBehaviour
{
	[SerializeField] private LayerMask Surface;

	private NavMeshAgent _agent;
	private Character _character;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_character = GetComponent<Character>();

		Observable.EveryUpdate().Where(_ => (_agent.remainingDistance <= _agent.stoppingDistance) && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)).Subscribe(_ =>
		{
			_character.Animation.SetRunning(false);
		});
	}

	public void MoveToCursorPosition()
	{
		_agent.SetDestination(MainCamera.Instance.MousePositionInWorldSpace);

		_character.Animation.SetRunning(true);
	}
}
