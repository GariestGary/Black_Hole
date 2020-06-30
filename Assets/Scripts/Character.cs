using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private bool Player = false;

	public MovementComponent Movement { get; private set; }
	public CharacterAnimation Animation { get; private set; }

	private void Awake()
	{
		Movement = GetComponent<MovementComponent>();
		Animation = GetComponent<CharacterAnimation>();

		if (Player)
		{
			GameManager.Instance.InitializePlayer(this);
		}


	}
}
