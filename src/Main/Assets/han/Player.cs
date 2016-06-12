using System;
using UnityEngine;

namespace Model
{
	public class Player : MonoBehaviour
	{
		public GameObject shield;
		public void InvokeShield(){
			shield.GetComponent<Rigidbody2D> ().AddTorque (20000);
		}
		public void Forward(float force){
			GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, force));
		}

		public void Rotate(float force){
			GetComponent<Rigidbody2D> ().AddTorque (force);
		}
	}
}

