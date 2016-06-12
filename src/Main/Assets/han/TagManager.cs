using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Model
{
	public class TagManager : MonoBehaviour, ITagManager, IEventSenderVerifyProxyDelegate
	{
		public int objectCount;

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
		}

		public void Unmanage(ITagObject player){
			_players.Remove (player);
		}

		public IEnumerable<ITagObject> FindObjectsWithTag(string tag){
			return 
				from obj in _players
				where obj.Tag == tag
				select obj;
		}

		public ITagObject FindObjectWithTagAndSeqID(string tag, int seqid){
			IEnumerable<ITagObject> a = 
				from obj in _players
				where obj.Tag == tag && obj.SeqID == seqid
				select obj;
			return a.FirstOrDefault (null);
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
		
		// Update is called once per frame
		void Update ()
		{
			objectCount = _players.Count;
		}
	}
}