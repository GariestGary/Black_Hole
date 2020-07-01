using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }

	private Controls _controls;
	private void Awake()
	{
		_controls = new Controls();

		Instance = this;
	}

	public void InitializeControls()
	{
		_controls.Player.LeftClick.performed += _ => GameManager.Instance.Player.LeftClick();
		_controls.Player.RightClick.performed += _ => GameManager.Instance.Player.RightClick();
	}

	private void OnEnable()
	{
		_controls.Enable();
	}

	private void OnDisable()
	{
		_controls.Disable();
	}
}
