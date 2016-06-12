using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Model
{
	public class TagManager : MonoBehaviour, ITagManager, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy _sender;
		HashSet<ITagObject> _players = new HashSet<ITagObject>();
		int idx;

		public void OnAddReceiver(object receiver){

		}
		public void OnRemoveReceiver(object receiver){
			
		}
		public bool VerifyReceiverDelegate(object receiver){
			return false;
		}

		public void Manage(ITagObject player){
			player.SeqID = idx++;
			_players.Add (player);
			Debug.Log (player.Tag+":"+player.SeqID);
		}

		public void Unmanage(ITagObject player){
			_players.Remove (player);
		}

		void Awake(){
			_sender = new EventSenderVerifyProxy (this);
		}
				
		void Start ()
		{
			GameContext.single.EventManager.Add(_sender);
		}

		void Destroy(){
			GameContext.single.EventManager.Remove(_sender);
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}