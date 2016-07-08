using System;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public enum MoveToTargetAndAttackActionStep{
		Pendding,
		MoveToTarget,
		Attack,
		End
	}

	public class MoveToTargetAndAttackAction : IFireAction
	{
		public FireSystem FireSystem{ get; set; }
		public GameObject Self{ get; set; }
		public GameObject Target{ get; set; }
		public MoveToTargetAndAttackActionStep Step{ get; set; }
		float animationTimer = 0f;

		bool IsCloseToTarget(){
			return false;
		}
		void MoveToTarget(){
			FireSystem.MoveToPositionStep (Target.transform.position);
		}
		public void Init(){
			Step = MoveToTargetAndAttackActionStep.Pendding;
		}
		public bool Update(){
			switch (Step) {
			case MoveToTargetAndAttackActionStep.Pendding:
				if (IsCloseToTarget () == false) {
					Step = MoveToTargetAndAttackActionStep.MoveToTarget;
				} else {
					Step = MoveToTargetAndAttackActionStep.Attack;
				}
				break;
			case MoveToTargetAndAttackActionStep.MoveToTarget:
				if (IsCloseToTarget () == false) {
					MoveToTarget ();
				} else {
					Step = MoveToTargetAndAttackActionStep.Attack;
				}
				break;
			case MoveToTargetAndAttackActionStep.Attack:
				if (animationTimer == 0) {
					FireSystem.SwordAttack (Target);
				} else if (animationTimer > 0.5) {
					Step = MoveToTargetAndAttackActionStep.End;
					return true;
				}
				animationTimer += Time.deltaTime;
				break;
			}
			return false;
		}
		public void Cancel(){
			Step = MoveToTargetAndAttackActionStep.Pendding;
		}
	}
}

