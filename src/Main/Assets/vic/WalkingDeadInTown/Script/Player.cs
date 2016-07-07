using UnityEngine;
using System.Collections;

namespace WalkingDeadInTown.View{
	public class Player : MonoBehaviour {

		public void SetState( string state ){
			switch (state) {
			case "Walking":
				transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
				break;
			case "Running":
				transform.Find ("BodyState").GetComponent<TextMesh> ().text = state;
				transform.Find ("FootState").GetComponent<TextMesh> ().text = state;
				break;
			case "Fire":
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

		public void SetPosition( float x, float y ){
			bool isUp = ( y - transform.localPosition.y ) > 0;
			SetDirect (isUp);
			transform.localPosition = new Vector3 (x, y, 0);
		}

		// Use this for initialization
		void Start () {
			SetState ("Running");
			SetPosition (1, 3);
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
