using System;
using System.Collections.Generic;
using UnityEngine;
using Han.Util;

namespace AIRace.Model
{
	public class UseControl : MonoBehaviour, IKeyListener
	{
		public GameObject car;
		public List<float> action;

		void Awake(){
			EventManager.Singleton.Add(this);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove(this);
		}
		void Update(){
			for (var i = 0; i < action.Count; ++i) {
				action [i] += (0.5f - action [i])*0.5f;
			}
			Car c = car.GetComponent<Car> ();
			c.PerformAction (action.ToArray(), Time.deltaTime);
		}

		public void OnKeyDown(KeyCode code){

		}
		public void OnKeyHold(KeyCode code){
			switch (code) {
			case KeyCode.E:
				action [0] = 1;
				break;
			case KeyCode.D:
				action [0] = 0;
				break;
			case KeyCode.S:
				action [1] = 1;
				break;
			case KeyCode.F:
				action [1] = -1;
				break;
			}

			for (var i = 0; i < action.Count; ++i) {
				if (action [i] > 1) {
					action [i] = 1;
				}
				if (action [i] < 0) {
					action [i] = 0;
				}
			}
		}
		public void OnKeyUp(KeyCode code){

		}
	}
}

