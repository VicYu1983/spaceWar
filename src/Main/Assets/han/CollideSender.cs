using System;
using UnityEngine;

namespace Model
{
	public class CollideSender : MonoBehaviour
	{
		void OnCollisionEnter2D(Collision2D coll) {
			SendMessageUpwards ("OnCollisionEnter2Dx", coll);
		}
	}
}

