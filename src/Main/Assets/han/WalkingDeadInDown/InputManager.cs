using System;
using Han.Util;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace WalkingDeadInDown.Model
{
	public class InputManager : MonoBehaviour, ITagManagerListener, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy proxy;

		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
			EventManager.Singleton.Add (this);
			EventManager.Singleton.Add (proxy);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (proxy);
			EventManager.Singleton.Remove (this);
		}

		public void OnAddReceiver(object receiver){

		}
		public void OnRemoveReceiver(object receiver){

		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is IInputListener;
		}

		List<GameObject> cantouchs = new List<GameObject>();

		void Update(){
			if (Input.touchCount > 0 ) {
				if( Input.GetTouch(0).phase == TouchPhase.Began ){
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
					foreach (GameObject go in cantouchs) {
						Collider col = go.GetComponent<Collider> ();
						if (col != null) {
							RaycastHit hit;
							if (col.Raycast (ray, out hit, 100)) {
								foreach (IInputListener obj in proxy.Receivers) {
									obj.OnInputTouchBegin (go);
								}
							}
						}
					}
				}
				if (Input.GetTouch (0).phase == TouchPhase.Stationary || Input.GetTouch (0).phase == TouchPhase.Moved) {
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
					foreach (GameObject go in cantouchs) {
						Collider col = go.GetComponent<Collider> ();
						if (col != null) {
							RaycastHit hit;
							if (col.Raycast (ray, out hit, 100)) {
								foreach (IInputListener obj in proxy.Receivers) {
									obj.OnInputTouchHold (go);
								}
							}
						}
					}
				}
			}
		}
		public ITagManager TagManager{ set; get; }

		public void OnManage(ITagObject obj){
			if (obj.Belong.GetComponent<CanTouch> () != null) {
				cantouchs.Add (obj.Belong);
			}
		}

		public void OnUnManage(ITagObject obj){
			if (obj.Belong.GetComponent<CanTouch> () != null) {
				cantouchs.Remove (obj.Belong);
			}
		}
	}
}

