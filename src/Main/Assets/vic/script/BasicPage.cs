using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Model;
namespace View
{
	public class BasicPage: MonoBehaviour, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy _sender;

		Button[] btns;

		public BasicPage ()
		{
			
		}

		public void OnAddReceiver(object receiver){
			
		}
		public void OnRemoveReceiver(object receiver){
			
		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is IBasicPageListener;
		}

		void Start ()
		{
			GameContext.single.EventManager.Add(_sender);

			btns = transform.GetComponentsInChildren<Button> ();

			foreach (Button btn in btns) {
				btn.onClick.AddListener (delegate() {
					this.OnBtnClick (btn.gameObject);
				});
			}
		}

		void OnBtnClick( GameObject sender ){
			foreach (var obj in _sender.Receivers) {
				(obj as IBasicPageListener).OnClick (this.name, sender.name);
			}
		}

		void OnDestroy(){
			GameContext.single.EventManager.Remove(_sender);

			foreach (Button btn in btns) {
				btn.onClick.RemoveAllListeners ();
			}
		}

		void Awake(){
			_sender = new EventSenderVerifyProxy (this);
		}
	}
}

