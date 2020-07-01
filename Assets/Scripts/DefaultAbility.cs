using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

public interface IAbility
{
	void UseAbility(Vector3 position);
	void OnAbilitySelected();
	void Break();

	float Radius { get; }
	float ActivationRadius { get; }
}