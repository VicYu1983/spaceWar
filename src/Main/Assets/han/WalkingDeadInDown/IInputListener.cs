using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public interface IInputListener
	{
		void OnInputTouchBegin(GameObject go);
		void OnInputTouchHold(GameObject go);
	}
}

