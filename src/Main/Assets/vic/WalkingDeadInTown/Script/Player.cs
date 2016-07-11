using UnityEngine;
using System.Collections;
using WalkingDeadInDown.Model;
using Han.Util;

namespace WalkingDeadInTown.View{
	public class Player : MonoBehaviour {

		Vector3 oldPosition;
		Vector3 usingLeftScale = new Vector3 (-.1f, .1f, .1f);
		Vector3 usingRightScale = new Vector3 (.1f, .1f, .1f);

		string bodyState = "none";
		float animationTimer = 0f;

		public void SetState( string state ){
			switch (state) {
			case "Walking":
				if( bodyState == "none" )
					transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
				break;
			case "Running":
				if( bodyState == "none" )
					transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
				break;
			case "Fire":
			case "SpecFire":
			case "Stock":
			case "SwordAttack":
				animationTimer = 0;

				bodyState = state;
				transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				break;
			}
		}

		public void SetPosition( Vector3 pos ){
			Vector3 posoff = pos - transform.localPosition;
			if (posoff.y != 0) {
				bool isUp = posoff.y > 0;
				SetUpDown (isUp);
			}
			if (posoff.x != 0) {
				bool isLeft = posoff.x < 0;
				SetLeftRight (isLeft);
			}
			if (posoff.magnitude > 1) {
				SetState ("Running");
			} else {
				SetState ("Walking");
			}

			transform.localPosition = pos;
		}
			
		void Awake(){
			EventManager.Singleton.Add (this);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}

		void Update(){
			animationTimer += Time.deltaTime;
			if (animationTimer > .5f) {
				bodyState = "none";
			}
		}

		void SetUpDown( bool isUp ){
			if (isUp) {
				transform.Find ("DirectState").GetComponent<TextMesh> ().text = "UP";
			} else {
				transform.Find ("DirectState").GetComponent<TextMesh> ().text = "Down";
			}
		}

		void SetLeftRight( bool isLeft ){
			if (isLeft) {
				transform.Find ("Body").transform.localScale = usingLeftScale;
				transform.Find ("Foot").transform.localScale = usingLeftScale;
			} else {
				transform.Find ("Body").transform.localScale = usingRightScale;
				transform.Find ("Foot").transform.localScale = usingRightScale;
			}
		}
	}
}
