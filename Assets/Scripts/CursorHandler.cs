using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.EventSystems;

public class CursorHandler : MonoBehaviour
{
    public static CursorHandler Instance { get; private set; }

    [SerializeField] private DecalProjector SelectionCircle;

    public bool isOverUI { get { return EventSystem.current.IsPointerOverGameObject(); } private set { } }

    private void Awake()
	{
        Instance = this;

        SetDefault();
	}

	private void LateUpdate()
	{
        transform.position = MainCamera.Instance.MousePositionInWorldSpace;
	}

	public void SetDefault()
    {
        SelectionCircle.gameObject.SetActive(false);
    }

    public void SetCircle(float radius)
    {
        SelectionCircle.gameObject.SetActive(true);
        SelectionCircle.size = new Vector3(radius * 2, radius * 2, 100);
    }
}
