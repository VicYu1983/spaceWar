using UnityEngine;
using System.Collections;
using WalkingDeadInDown.Model;
using Han.Util;

namespace WalkingDeadInTown.View{
	public class Player : MonoBehaviour {

		public GameObject body;
		public GameObject foot;

		Vector3 oldPosition;
		Vector3 usingLeftScale = new Vector3 (-3f, 3f, 3f);
		Vector3 usingRightScale = new Vector3 (3f, 3f, 3f);

		string bodyState = "none";
		float animationTimer = 0f;

		/*
		 * 701
		 * 602
		 * 543
		 */
		int dir = 0;

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
			float speed = posoff.magnitude;
			if (posoff.magnitude == 0) {
				//stand
			} else {
				//walk
				if (posoff.y == 0 && posoff.x > 0) {
					dir = 2;
				} else if (posoff.y == 0 && posoff.x < 0) {
					dir = 6;
				} else if (posoff.x == 0 && posoff.y > 0) {
					dir = 0;
				} else if (posoff.x == 0 && posoff.y < 0) {
					dir = 4;
				}
			}

			ChangeAnimation (speed);
			transform.localPosition = pos;
		}
			
		void Awake(){
			EventManager.Singleton.Add (this);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}

		void Start(){
			
		}

		void Update(){
			animationTimer += Time.deltaTime;
			if (animationTimer > .5f) {
				bodyState = "none";
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

		void ChangeAnimation( float speed ){
			print (speed);
			if (speed == 0) {
				switch (dir) {
				case 0:
					body.GetComponent<Animator> ().Play ("body_stand_back");
					foot.GetComponent<Animator> ().Play ("foot_stand_back");
					break;
				case 1:
					break;
				case 2:
					body.GetComponent<Animator> ().Play ("body_stand_right");
					foot.GetComponent<Animator> ().Play ("foot_stand_right");
					break;
				case 3:
					break;
				case 4:
					body.GetComponent<Animator> ().Play ("body_stand_front");
					foot.GetComponent<Animator> ().Play ("foot_stand_front");
					break;
				case 5:
					break;
				case 6:
					body.GetComponent<Animator> ().Play ("body_stand_right");
					foot.GetComponent<Animator> ().Play ("foot_stand_right");
					break;
				case 7:
					break;
				}
			} else {
				switch (dir) {
				case 0:
					body.GetComponent<Animator> ().Play ("body_walk_back");
					foot.GetComponent<Animator> ().Play ("foot_walk_back");
					break;
				case 1:
					break;
				case 2:
					body.GetComponent<Animator> ().Play ("body_walk_right");
					foot.GetComponent<Animator> ().Play ("foot_walk_right");
					break;
				case 3:
					break;
				case 4:
					body.GetComponent<Animator> ().Play ("body_walk_front");
					foot.GetComponent<Animator> ().Play ("foot_walk_front");
					break;
				case 5:
					break;
				case 6:
					body.GetComponent<Animator> ().Play ("body_walk_right");
					foot.GetComponent<Animator> ().Play ("foot_walk_right");
					break;
				case 7:
					break;
				}
			}

			switch (dir) {
			case 0:
				break;
			case 1:
			case 2:
			case 3:
				SetLeftRight (false);
				break;
			case 4:
				break;
			case 5:
			case 6:
			case 7:
				SetLeftRight (true);
				break;
			}
		}
	}
}
