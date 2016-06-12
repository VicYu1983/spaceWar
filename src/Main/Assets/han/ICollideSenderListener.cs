using System;
using UnityEngine;

namespace Model
{
	public interface ICollideSenderListener
	{
		void OnCollideEnter(Collision2D coll);
	}
}

