using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public interface IInputManagerListener
	{
		//void OnInputTouchObject(TouchPhase phase, GameObject go);
		void OnInputMouseObject(TouchPhase phase, int button, GameObject go); 
	}
}

