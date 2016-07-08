using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public interface IFireSystemListener
	{
		void OnFireSystemFire (FireSystem fs, GameObject target, object info);
		void OnFireSystemSpecFire (FireSystem fs, GameObject target, object info);
		void OnFireSystemStock (FireSystem fs, GameObject target, object info);
		void OnFireSystemSwordAttack(FireSystem fs, GameObject target, object info);
	}
}

