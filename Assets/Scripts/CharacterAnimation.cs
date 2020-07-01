using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
	[SerializeField] private Animator _animator;

	public void SetRunning(bool state)
	{
		_animator.SetBool("Running", state);
	}

	public void SetCasting(bool state)
	{
		_animator.SetBool("Casting", state);
	}

	public void SetStunned(bool state)
	{
		_animator.SetBool("Stunned", state);
	}

}
