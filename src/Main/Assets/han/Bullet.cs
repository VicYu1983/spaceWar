using System;
using UnityEngine;

namespace Model
{
	public class Bullet : MonoBehaviour
	{
		public GameObject body;
		public float timer;

		void Update(){
			timer += Time.deltaTime;
			if (timer > 10) {
				Destroy (gameObject);
			}
		}
	}
}

