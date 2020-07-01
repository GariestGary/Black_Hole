using System;
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

	public bool Running { get; private set; }

	public event Action ReachedDestination = delegate { };

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_character = GetComponent<Character>();

		Observable.EveryUpdate().Where(_ => _agent.isOnNavMesh && (_agent.remainingDistance <= _agent.stoppingDistance) && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)).Subscribe(_ =>
		{
			if (Running)
			{
				ReachedDestination();
			}

			Running = false;
			_character.Animation.SetRunning(Running);
		});
	}

	public void Move(Vector3 position)
	{
		_agent.SetDestination(position);
		Running = true;
		_character.Animation.SetRunning(Running);
	}

	public void Stop()
	{
		_agent.SetDestination(transform.position);
	}
}
