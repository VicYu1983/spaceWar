using System;
using UnityEngine;

namespace Han.Util
{
	public interface IKeyListener
	{
		void OnKeyDown(KeyCode code);
		void OnKeyHold(KeyCode code);
		void OnKeyUp(KeyCode code);
	}
}

