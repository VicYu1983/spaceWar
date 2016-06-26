using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SpaceWar.Model;
using Han.Util;

namespace Vic.Util
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
			EventManager.Singleton.Add(_sender);

			btns = transform.GetComponentsInChildren<Button> ();

			Func<Button,UnityEngine.Events.UnityAction> delegateClick = delegate (Button target)  
			{  
				return delegate {
					this.OnBtnClick( target.gameObject );
				};
			};  
				
			foreach (Button btn in btns) {
				btn.onClick.AddListener (delegateClick (btn));
			}
		}

		void OnBtnClick( GameObject sender ){
			print ("OnBtnClick: " + sender.name);
			foreach (var obj in _sender.Receivers) {
				(obj as IBasicPageListener).OnClick ((PageName)Enum.Parse( typeof( PageName ), this.name ), sender.name);
			}
		}

		public void OnAnimationTrigger( string name ){
			print ("OnAnimationTrigger " + (PageName)Enum.Parse( typeof( PageName ), this.name ) + ": " + name);
			switch (name) {
			case "End":
				Destroy (this.gameObject);
				break;
			default:
				foreach (var obj in _sender.Receivers) {
					(obj as IBasicPageListener).OnAnimationTrigger ((PageName)Enum.Parse( typeof(PageName), this.name ), name);
				}
				break;
			}
		}

		void OnDestroy(){
			EventManager.Singleton.Remove(_sender);

			foreach (Button btn in btns) {
				btn.onClick.RemoveAllListeners ();
			}
		}

		void Awake(){
			_sender = new EventSenderVerifyProxy (this);
		}
	}
}

