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
}
