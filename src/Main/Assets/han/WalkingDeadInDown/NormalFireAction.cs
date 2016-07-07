using System;
using UnityEngine;


namespace WalkingDeadInDown.Model
{
	public class NormalFireAction : IFireAction
	{
		public FireSystem FireSystem{ get; set; }
		public GameObject Target{ get; set; }
		public float animationTimer = 0f;
		public void Init(){
			
		}
		public bool Update(){
			if (animationTimer == 0) {
				if (FireSystem.IsAlreadyTargetTimes ()) {
					FireSystem.Action (new SpecFireAction (){ Target = Target }, true);
				} else {
					FireSystem.Fire (Target);
					FireSystem.RecordFireTarget (Target);
				}
			} else if (animationTimer > 0.2) {
				return true;
			}
			animationTimer += Time.deltaTime;
			return false;
		}
		public void Cancel(){
			
		}
	}
}

