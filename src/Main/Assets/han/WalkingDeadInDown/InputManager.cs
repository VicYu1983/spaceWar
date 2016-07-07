using System;
using Han.Util;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace WalkingDeadInDown.Model
{
	public class InputManager : MonoBehaviour, ITagManagerListener
	{
		List<GameObject> cantouchs = new List<GameObject>();
		FireSystem fireSystem;
		//Subject ontouch = new Subject();

		void Update(){
			if (Input.touchCount > 0 ) {
				if( Input.GetTouch(0).phase == TouchPhase.Began ){
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
					foreach (GameObject go in cantouchs) {
						Collider col = go.GetComponent<Collider> ();
						if (col != null) {
							RaycastHit hit;
							col.Raycast (ray, out hit, 100);
						}
					}
				}
				if (Input.GetTouch (0).phase == TouchPhase.Stationary || Input.GetTouch (0).phase == TouchPhase.Moved) {
					
				}
			}
		}


		public ITagManager TagManager{ set; get; }

		public void OnManage(ITagObject obj){
			if (obj.Belong.GetComponent<CanTouch> () != null) {
				cantouchs.Add (obj.Belong);
			}
			if (obj.Tag == "playerFireSystem") {
				fireSystem = obj.Belong.GetComponent<FireSystem> ();
			}
		}

		public void OnUnManage(ITagObject obj){
			if (obj.Belong.GetComponent<CanTouch> () != null) {
				cantouchs.Remove (obj.Belong);
			}
			if (obj.Tag == "playerFireSystem") {
				fireSystem = null;
			}
		}
	}
}

