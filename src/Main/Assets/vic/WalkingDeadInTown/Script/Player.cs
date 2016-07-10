using UnityEngine;
using System.Collections;
using WalkingDeadInDown.Model;
using Han.Util;

namespace WalkingDeadInTown.View{
	public class Player : MonoBehaviour, IFireSystemListener {

		Vector3 oldPosition;

		public void SetState( string state ){
			switch (state) {
			case "Walking":
			//	transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
				break;
			case "Running":
			//	transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
				break;
			case "Fire":
			case "SpecFire":
			case "Stock":
			case "SwordAttack":
				transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				break;
			}
		}

		public void SetDirect( bool isUp ){
			if (isUp) {
				transform.Find ("DirectState").GetComponent<TextMesh> ().text = "UP";
			} else {
				transform.Find ("DirectState").GetComponent<TextMesh> ().text = "Down";
			}
		}

		public void OnFireSystemFire (FireSystem fs, GameObject target, object info){
			SetState ("Fire");
		}

		public void OnFireSystemSpecFire (FireSystem fs, GameObject target, object info){
			SetState ("SpecFire");
		}

		public void OnFireSystemStock (FireSystem fs, GameObject target, object info){
			SetState ("Stock");
		}

		public void OnFireSystemSwordAttack(FireSystem fs, GameObject target, object info){
			SetState ("SwordAttack");
		}

		void Awake(){
			EventManager.Singleton.Add (this);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}

		void JudgeDirect(){
			if (oldPosition == null) {
				oldPosition = transform.position;
			} else {
				Vector3 posoff = transform.position - oldPosition;
				if (posoff.y != 0) {
					bool isUp = posoff.y > 0;
					SetDirect (isUp);
				}
				if (posoff.magnitude > 2) {
					SetState ("Running");
				} else {
					SetState ("Walking");
				}

				oldPosition = transform.position;
			}
		}
		
		// Update is called once per frame
		void Update () {
			JudgeDirect ();
		}
	}
}
