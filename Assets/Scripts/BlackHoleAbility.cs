using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAbility : MonoBehaviour, IAbility
{
    [SerializeField] private float _duration;
    [SerializeField] private float _activationRadius;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _blackHoleEffect;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private float _effectRotationDuration;

    private GameObject _effect;
    private List<Character> _affectedCharacters;
    private List<Tween> _tweens;

	public float Radius => _radius;
	public float ActivationRadius => _activationRadius;

	private void Awake()
	{
        _affectedCharacters = new List<Character>();
        _tweens = new List<Tween>();
	}
	public void Break()
	{
        StartCoroutine(EffectDestroyDelay(0));
    }

	public void OnAbilitySelected()
	{
        CursorHandler.Instance.SetCircle(_radius);
	}

	public void UseAbility(Vector3 position)
    {
        if (_effect == null)
        { 
            _effect = Instantiate(_blackHoleEffect, position, Quaternion.identity);

            
            GameManager.Instance.Player.SetCast(true);
            StartCoroutine(EffectDestroyDelay(_duration));

            Collider[] chars = Physics.OverlapSphere(position, _radius, EnemyLayer);

			for (int i = 0; i < chars.Length; i++)
			{
                _affectedCharacters.Add(chars[i].GetComponent<Character>());
                _affectedCharacters[i].SetStun(true);
                _affectedCharacters[i].transform.SetParent(_effect.transform);

                _tweens.Add(_affectedCharacters[i].transform.DOLocalMove(Vector3.zero, _effectRotationDuration));
            }

            
        }
    }

    private IEnumerator EffectDestroyDelay(float duration)
    {
        _tweens.Add(_effect.transform.DORotate(new Vector3(0, 359, 0), _effectRotationDuration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear));
        yield return new WaitForSeconds(duration);
		for (int i = 0; i < _tweens.Count; i++)
		{
            _tweens[i].Kill();
		}
        _tweens.Clear();

        Destroy(_effect);
        GameManager.Instance.Player.SetCast(false);

		for (int i = 0; i < _affectedCharacters.Count; i++)
		{
            _affectedCharacters[i].SetStun(false);
            _affectedCharacters[i].transform.SetParent(null);
		}

        _affectedCharacters.Clear();
    }
    
}
