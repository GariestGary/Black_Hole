using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	public static MainCamera Instance { get; private set; }

	public Transform Target { private get; set; }

	[SerializeField] private bool _followPlayer;
	[SerializeField] private Vector3 _positionOffset;
	[SerializeField] private Vector3 _rotation;

	private void Awake()
	{
		Instance = this;
	}

	private void LateUpdate()
	{
		if (_followPlayer)
		{
			transform.position = Target.position + _positionOffset;
		}
		else
		{
			transform.position = Vector3.zero + _positionOffset;
		}

		transform.rotation = Quaternion.Euler(_rotation);
	}

	public void CameraInitialize()
	{ 
		
	}
}
