using System;
using UnityEngine;
using System.Collections.Generic;
using Han.Util;

namespace WalkingDeadInDown.Model
{
	public class FireSystem : MonoBehaviour, IEventSenderVerifyProxyDelegate, IInputListener
	{
		EventSenderVerifyProxy proxy;

		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
			EventManager.Singleton.Add (proxy);
			EventManager.Singleton.Add (this);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
			EventManager.Singleton.Remove (proxy);
		}

		public void OnAddReceiver(object receiver){
			
		}
		public void OnRemoveReceiver(object receiver){

		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is IFireSystemListener;
		}

		public int shootTimesForSpecShoot = 3;
		public List<GameObject> history = new List<GameObject> ();
		public IFireAction action;

		public bool Action(IFireAction action, bool force = false){
			if (this.action != null) {
				if (force) {
					this.action.Cancel ();
				} else {
					return false;
				}
			}
			action.FireSystem = this;
			action.Init ();
			return true;
		}

		public IFireAction CurrAction{
			get{
				return action;
			}
		}

		public void RecordFireTarget(GameObject target){
			history.Add (target);
			if (history.Count > shootTimesForSpecShoot) {
				history.RemoveAt (0);
			}
		}

		public bool IsAlreadyTargetTimes(){
			if (history.Count >= shootTimesForSpecShoot) {
				GameObject obj = history[0];
				for (var i = 1; i < history.Count; ++i) {
					if (obj != history [i]) {
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public void Stock(GameObject target){
			foreach (IFireSystemListener obj in proxy.Receivers) {
				obj.OnFireSystemStock (this, target, null);
			}
		}

		public void Fire(GameObject target){
			foreach (IFireSystemListener obj in proxy.Receivers) {
				obj.OnFireSystemFire (this, target, null);
			}
		}

		public void SpecFire(GameObject target){
			foreach (IFireSystemListener obj in proxy.Receivers) {
				obj.OnFireSystemSpecFire (this, target, null);
			}
		}

		void Update(){
			if (action != null) {
				if (action.Update ()) {
					action = null;
				}
			}
		}

		public void OnInputTouchBegin(GameObject go){
			Action (new NormalFireAction (){ Target = go });
		}

		public void OnInputTouchHold(GameObject go){
			Action (new NormalFireAction (){ Target = go });
		}
	}
}

