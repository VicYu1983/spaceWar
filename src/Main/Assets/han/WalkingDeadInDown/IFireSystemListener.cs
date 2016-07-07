using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public interface IFireSystemListener
	{
		void OnFireSystemFire (FireSystem fs, GameObject target, object obj);
		void OnFireSystemSpecFire (FireSystem fs, GameObject target, object obj);
		void OnFireSystemStock (FireSystem fs, GameObject target, object obj);
	}
}

