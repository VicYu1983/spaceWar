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
			for (var i = 0; i < Action.Length; ++i) {
				Action [i] = 0.5f;
			}
		}

		void Update(){
			UpdateState ();
		}

		void UpdateState(){
			State [0] = transform.position.x / 10;
			State [1] = transform.position.y / 10;
			State [2] = transform.rotation.eulerAngles.z/ 360f;
		}

		public void PerformAction(float[] action, float deltaTime){
			var forward = (action [0]-0.5f)*2 * maxSpeed;
			var rotation = (action [1]-0.5f)*180;
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

