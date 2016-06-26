using System;
using UnityEngine;
using Han.Util;

namespace Han.Util
{
	public class KeyManager : MonoBehaviour, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy _sender;

		public void OnAddReceiver(object receiver){
			
		}
		public void OnRemoveReceiver(object receiver){

		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is IKeyListener;
		}
			
		void Awake(){
			_sender = new EventSenderVerifyProxy (this);
		}

		void Start ()
		{
			EventManager.Singleton.Add(_sender);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove(_sender);
		}

		KeyCode[] checkKeys = new KeyCode[]{
			KeyCode.UpArrow, 
			KeyCode.DownArrow, 
			KeyCode.LeftArrow, 
			KeyCode.RightArrow,
			KeyCode.F,
			KeyCode.Space,
			KeyCode.L
		};

		void Update(){
			foreach( KeyCode key in checkKeys){
				bool check = Input.GetKeyDown (key);
				if (check) {
					foreach (object obj in _sender.Receivers) {
						(obj as IKeyListener).OnKeyDown (key);
					}
				}
				check = Input.GetKey (key);
				if (check) {
					foreach (object obj in _sender.Receivers) {
						(obj as IKeyListener).OnKeyHold (key);
					}
				}
				check = Input.GetKeyUp (key);
				if (check) {
					foreach (object obj in _sender.Receivers) {
						(obj as IKeyListener).OnKeyUp (key);
					}
				}
			}
		}
	}
}

