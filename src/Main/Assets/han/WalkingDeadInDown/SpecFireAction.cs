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
			if (animationTimer == 0) {
				FireSystem.Stock (Target);
			}
			if (lastTimer < 1 && animationTimer >= 1) {
				FireSystem.SpecFire (Target);
			} else if( animationTimer > 1.2 ){
				return true;
			}
			lastTimer = animationTimer;
			animationTimer += Time.deltaTime;
			return false;
		}
		public void Cancel(){
			
		}
	}
}

