using System;
using UnityEngine;

namespace Model
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
			GameContext.single.EventManager.Add(_sender);
		}

		void OnDestroy(){
			GameContext.single.EventManager.Remove(_sender);
		}

		KeyCode[] checkKeys = new KeyCode[]{
			KeyCode.UpArrow, 
			KeyCode.DownArrow, 
			KeyCode.LeftArrow, 
			KeyCode.RightArrow,
			KeyCode.F,
			KeyCode.Space
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

