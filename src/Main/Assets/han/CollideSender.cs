using System;
using UnityEngine;

namespace Model
{
	public class CollideSender : MonoBehaviour, IEventSenderVerifyProxyDelegate
	{
		public GameObject belong;

		EventSenderVerifyProxy proxy;

		public GameObject Belong{ get{ return belong; } }

		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
		}
		void Start (){
			GameContext.single.EventManager.Add(proxy);
		}
		void OnDestroy(){
			GameContext.single.EventManager.Remove(proxy);
		}

		public void OnAddReceiver(object receiver){

		}
		public void OnRemoveReceiver(object receiver){

		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is ICollideSenderListener;
		}

		void OnCollisionEnter2D(Collision2D coll) {
			foreach (var g in proxy.Receivers) {
				(g as ICollideSenderListener).OnCollideEnter (coll);
			}
		}

		void OnTriggerEnter2D(Collider2D coll){
			
		}
	}
}

