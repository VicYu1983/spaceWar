using UnityEngine;
using System.Collections;
using WalkingDeadInDown.Model;
using Han.Util;

namespace WalkingDeadInTown.View{
	public class Player : MonoBehaviour {

		public GameObject body;
		public GameObject foot;
		public float dir_fac = 20;

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
			case "Stand":
				if (bodyState == "none") {
					transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
					transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
					StandAnimation (true);

				} else {
					transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
					StandAnimation (false);
				}
				DefineDir ();
				break;
			case "Walking":
				if (bodyState == "none") {
					transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
					transform.Find ("FootState").GetComponent<TextMesh> ().text = state;

					WalkAnimation (true);
				} else {
					transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
					WalkAnimation (false);
				}
				DefineDir ();
				break;
			case "Running":
				break;
			case "Fire":
				animationTimer = 0;

				bodyState = state;
				transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				FireAnimation ();
				break;
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

				float radian = Mathf.Atan2 (posoff.y, posoff.x);
				float degree = radian / Mathf.PI * 180;
				degree += 180;

				if (degree < (0 + dir_fac) || degree > (360 - dir_fac)) {
					dir = 6;
				} else if (degree > 0 + dir_fac && degree < 90 - dir_fac) {
					dir = 5;
				} else if (degree > 90 - dir_fac && degree < 90 + dir_fac) {
					dir = 4;
				} else if (degree > (90 + dir_fac) && degree < (180 - dir_fac)) {
					dir = 3;
				} else if (degree > 180 - dir_fac && degree < 180 + dir_fac) {
					dir = 2;
				} else if (degree > 180 + dir_fac && degree < 270 - dir_fac) {
					dir = 1;
				} else if (degree > 270 - dir_fac && degree < 270 + dir_fac) {
					dir = 0;
				} else if (degree > 270 + dir_fac && degree < 360 - dir_fac) {
					dir = 7;
				}
			}

			if (speed == 0) {
				SetState ("Stand");
			} else {
				SetState ("Walking");
			}
			
			//ChangeAnimation (speed);
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

		void DefineDir(){
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

		void StandAnimation( bool alsoBody = true ){
			switch (dir) {
			case 0:
				if( alsoBody )
					body.GetComponent<Animator> ().Play ("body_stand_back");
				foot.GetComponent<Animator> ().Play ("foot_stand_back");
				break;
			case 1:
			case 7:
				if( alsoBody )
					body.GetComponent<Animator> ().Play ("body_stand_back_right");
				foot.GetComponent<Animator> ().Play ("foot_stand_back_right");
				break;
			case 2:
			case 6:
				if( alsoBody )
					body.GetComponent<Animator> ().Play ("body_stand_right");
				foot.GetComponent<Animator> ().Play ("foot_stand_right");
				break;
			case 3:
			case 5:
				if( alsoBody )
					body.GetComponent<Animator> ().Play ("body_stand_front_right");
				foot.GetComponent<Animator> ().Play ("foot_stand_front_right");
				break;
			case 4:
				if( alsoBody )
					body.GetComponent<Animator> ().Play ("body_stand_front");
				foot.GetComponent<Animator> ().Play ("foot_stand_front");
				break;
			}
		}

		void FireAnimation(){
			switch (dir) {
			case 0:
				body.GetComponent<Animator> ().Play ("body_fire_back");
				break;
			case 1:
			case 7:
				body.GetComponent<Animator> ().Play ("body_fire_back_right");
				break;
			case 2:
			case 6:
				body.GetComponent<Animator> ().Play ("body_fire_right");
				break;
			case 3:
			case 5:
				body.GetComponent<Animator> ().Play ("body_fire_front_right");
				break;
			case 4:
				body.GetComponent<Animator> ().Play ("body_fire_front");
				break;
			}
		}

		void WalkAnimation( bool alsoBody = true ){
			switch (dir) {
			case 0:
				if (alsoBody)
					body.GetComponent<Animator> ().Play ("body_walk_back");
				foot.GetComponent<Animator> ().Play ("foot_walk_back");
				break;
			case 1:
			case 7:
				if (alsoBody)
					body.GetComponent<Animator> ().Play ("body_walk_back_right");
				foot.GetComponent<Animator> ().Play ("foot_walk_back_right");
				break;
			case 2:
			case 6:
				if (alsoBody)
					body.GetComponent<Animator> ().Play ("body_walk_right");
				foot.GetComponent<Animator> ().Play ("foot_walk_right");
				break;
			case 3:
			case 5:
				if (alsoBody)
					body.GetComponent<Animator> ().Play ("body_walk_front_right");
				foot.GetComponent<Animator> ().Play ("foot_walk_front_right");
				break;
			case 4:
				if (alsoBody)
					body.GetComponent<Animator> ().Play ("body_walk_front");
				foot.GetComponent<Animator> ().Play ("foot_walk_front");
				break;
			}
		}
	}
}
