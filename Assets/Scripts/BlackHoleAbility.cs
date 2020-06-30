using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAbility : MonoBehaviour, IAbility
{
    [SerializeField] private float _duration;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _blackHoleEffect;

    private GameObject _effect;

	public void OnAbilitySelected()
	{
        CursorHandler.Instance.SetCircle(_radius);
	}

	public void UseAbility()
    {
        if (_effect == null)
        { 
            _effect = Instantiate(_blackHoleEffect, MainCamera.Instance.MousePositionInWorldSpace, Quaternion.identity);
            StartCoroutine(EffectDestroyDelay());
        }
    }

    private IEnumerator EffectDestroyDelay()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(_effect);
    }
    
}
