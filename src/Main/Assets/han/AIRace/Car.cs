using System;
using UnityEngine;
using Han.Util;

namespace AIRace.Model
{
	public class Car : MonoBehaviour, ILearnTarget
	{
		public float maxSpeed = 5;

		public float[] State{ get; set; }
		public float[] Action{ get; set; }

		void Awake(){
			State = new float[3];
			Action = new float[2];
		}

		void Update(){
			UpdateState ();
		}

		void UpdateState(){
			State [0] = transform.position.x / 1;
			State [1] = transform.position.y / 1;
			State [2] = transform.rotation.eulerAngles.z;
		}

		public void PerformAction(float[] action, float deltaTime){
			var forward = action [0]* maxSpeed;
			var rotation = (action [1]-0.5f)*Mathf.PI;
			transform.position += transform.right * forward * deltaTime;

			var angle = transform.rotation.eulerAngles;
			angle.z += rotation * deltaTime;

			var rot = transform.rotation;
			rot.eulerAngles = angle;
			transform.rotation = rot;

			Action = action;
		}
	}
}

