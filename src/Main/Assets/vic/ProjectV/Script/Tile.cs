using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using Han.Util;

namespace ProjectV.View{
	public class Tile : MonoBehaviour, IEventSenderVerifyProxyDelegate
	{
		TransformGesture tg;
		EventSenderVerifyProxy proxy;

		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
			EventManager.Singleton.Add (proxy);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove (proxy);
		}

		private void OnEnable()
		{
			tg = GetComponent<TransformGesture> ();
			tg.TransformStarted += Tg_TransformStarted;
			tg.TransformCompleted += Tg_TransformCompleted;
		}
		
		private void OnDisable()
		{
			tg.TransformStarted -= Tg_TransformStarted;
			tg.TransformCompleted -= Tg_TransformCompleted;
		}

		void Tg_TransformStarted (object sender, System.EventArgs e)
		{
			foreach (ITileListener obj in proxy.Receivers) {
				obj.StartTouch ();
			}
		}

		void Tg_TransformCompleted (object sender, System.EventArgs e)
		{
			foreach (ITileListener obj in proxy.Receivers) {
				obj.EndTouch ();
			}
		}

		public bool VerifyReceiverDelegate(object receiver){
			return receiver is ITileListener;
		}
		public void OnAddReceiver(object receiver){
			
		}
		public void OnRemoveReceiver(object receiver){

		}
	}
}
