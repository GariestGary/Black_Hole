using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public InputManager inputManager { get; private set; }
	public Character Player { get; private set; }

	private void Awake()
	{
		inputManager = GetComponent<InputManager>();

		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance == this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	public void InitializePlayer(Character player)
	{
		Player = player;

		MainCamera.Instance.Target = Player.transform;

		MainCamera.Instance.CameraInitialize();
		inputManager.InitializeControls();
	}
}