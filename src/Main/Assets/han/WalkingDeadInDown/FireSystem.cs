using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public enum FireSystemState {
		Pendding,
		Normal
	}

	public class FireSystem : MonoBehaviour
	{
		public float Hot{ get; set; }
		public void Fire(Vector3 heading, object info){
			if (Hot > 0) {
				return;
			}
			Hot += 1;
		}

		void Update(){
			if (Hot > 0) {
				Hot -= 1* Time.deltaTime;
				if (Hot < 0) {
					Hot = 0;
				}
			}
		}
	}
}

