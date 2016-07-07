using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public class SpecFireAction : IFireAction
	{
		public FireSystem FireSystem{ get; set; }
		public GameObject Target{ get; set; }

		float lastTimer = 0f, animationTimer = 0f;

		public void Init(){
			animationTimer = 0;
		}
		public bool Update(){
			animationTimer += Time.deltaTime;
			if (animationTimer == 0) {
				FireSystem.Stock (Target);
			}
			if (lastTimer < 3 && animationTimer >= 3) {
				FireSystem.SpecFire (Target);
			} else if( animationTimer > 3.2 ){
				return true;
			}
			lastTimer = animationTimer;
			return false;
		}
		public void Cancel(){
			
		}
	}
}

