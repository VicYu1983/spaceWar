using System;
using UnityEngine;
using System.Collections.Generic;
using Han.Util;

namespace WalkingDeadInDown.Model
{
	public class FireSystem : MonoBehaviour, IEventSenderVerifyProxyDelegate, IInputManagerListener, IKeyListener
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
					this.action = null;
				} else {
					return false;
				}
			}
			if (action != null) {
				action.FireSystem = this;
				action.Init ();
				this.action = action;
				return true;
			}
			return false;
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

		public void SwordAttack(GameObject target){
			foreach (IFireSystemListener obj in proxy.Receivers) {
				obj.OnFireSystemSwordAttack (this, target, null);
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

		public void MoveToPositionStep(Vector3 pos){
			var curr = gameObject.transform.position;
			var v = pos - curr;
			v *= 0.2f;
			curr += v;
			gameObject.transform.position = curr;
		}

		void Update(){
			if (action != null) {
				if (action.Update ()) {
					action = null;
				}
			}
		}
			
		public void OnInputTouchObject(TouchPhase phase, GameObject go){
			switch (phase) {
			case TouchPhase.Stationary:
			case TouchPhase.Ended:
				Action (new NormalFireAction (){ Target = go });
				break;
			}
		}

		public void OnInputMouseObject(TouchPhase phase, int button, GameObject go){
			if (button == 0) {
				switch (phase) {
				case TouchPhase.Stationary:
				case TouchPhase.Ended:
					Action (new NormalFireAction (){ Target = go });
					break;
				}
			}

			if (button == 1) {
				Action (new MoveToTargetAndAttackAction (){ Target = go }, true);
			}
		}

		public void OnKeyDown(KeyCode code){

		}
		public void OnKeyHold(KeyCode code){
			if (code == KeyCode.F) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				var pos = ray.origin;
				pos.z = gameObject.transform.position.z;
				MoveToPositionStep (pos);
				Action (null, true);
			}

			if (code == KeyCode.W) {
				var pos = gameObject.transform.position;
				MoveToPositionStep (pos + new Vector3 (0, 1, 0));
				Action (null, true);
			}

			if (code == KeyCode.D) {
				var pos = gameObject.transform.position;
				MoveToPositionStep (pos + new Vector3 (1, 0, 0));
				Action (null, true);
			}

			if (code == KeyCode.S) {
				var pos = gameObject.transform.position;
				MoveToPositionStep (pos + new Vector3 (0, -1, 0));
				Action (null, true);
			}

			if (code == KeyCode.A) {
				var pos = gameObject.transform.position;
				MoveToPositionStep (pos + new Vector3 (-1, 0, 0));
				Action (null, true);
			}
		}
		public void OnKeyUp(KeyCode code){

		}
	}
}

