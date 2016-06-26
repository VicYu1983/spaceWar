using System;
using UnityEngine;

namespace Han.Util
{
	public interface ICollideSenderListener
	{
		void OnCollideEnter(Collision2D coll);
	}
}

