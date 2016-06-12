using System;
using UnityEngine;

namespace Model
{
	public class RemoveAtDuration : MonoBehaviour
	{
		public float duration = 10;
		public float timer;
		void Update(){
			timer += Time.deltaTime;
			if (timer > duration) {
				Destroy (gameObject);
			}
		}
	}
}

