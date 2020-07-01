using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.EventSystems;
using System;
using UniRx;

public class CursorHandler : MonoBehaviour
{
    public static CursorHandler Instance { get; private set; }

    [SerializeField] private Material _circle;
    [SerializeField] private Material _arrow;
    [SerializeField] private DecalProjector _selection;

    private IDisposable _cursorUpdater;

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
        transform.rotation = Quaternion.identity;
        GameManager.Dispose(_cursorUpdater);
        _selection.gameObject.SetActive(false);
    }

    public void SetCircle(float radius)
    {
        transform.rotation = Quaternion.identity;
        GameManager.Dispose(_cursorUpdater);
        _selection.material = _circle;
        _selection.gameObject.SetActive(true);
        _selection.size = new Vector3(radius * 2, radius * 2, 100);
    }

    public void SetArrow()
    {
        GameManager.Dispose(_cursorUpdater);
        _selection.material = _arrow;
        _selection.gameObject.SetActive(true);
        _selection.size = new Vector3(5, 5, 100);

        _cursorUpdater = Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.LookAt(GameManager.Instance.Player.transform);
        });
    }
}
