using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Han.Util;

namespace Han.Util
{
	public class TagManager : MonoBehaviour, ITagManager, IEventSenderVerifyProxyDelegate
	{
		public int objectCount;

		EventSenderVerifyProxy _sender;
		HashSet<ITagObject> _players = new HashSet<ITagObject>();
		int idx;

		public void OnAddReceiver(object receiver){
			(receiver as ITagManagerListener).TagManager = this;
		}
		public void OnRemoveReceiver(object receiver){
			
		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is ITagManagerListener;
		}

		public void Manage(ITagObject player){
			player.SeqID = idx++;
			_players.Add (player);
			foreach (var obj in _sender.Receivers) {
				(obj as ITagManagerListener).OnManage (player);
			}
		}

		public void Unmanage(ITagObject player){
			foreach (var obj in _sender.Receivers) {
				(obj as ITagManagerListener).OnUnManage (player);
			}
			_players.Remove (player);
		}

		public IEnumerable<ITagObject> FindObjectsWithTag(string tag){
			return 
				from obj in _players
				where obj.Tag == tag
				select obj;
		}

		public ITagObject FindObjectWithTagAndSeqID(string tag, int seqid){
			// 不需要判斷tag, seqId有唯一性
			IEnumerable<ITagObject> a = 
				from obj in _players
				where obj.SeqID == seqid
				select obj;
			return a.FirstOrDefault ();
		}

		public IEnumerable<ITagObject> FindObjectsWithComponent<T>(){
			IEnumerable<ITagObject> a = 
				from obj in _players
					where obj.Belong.GetComponent<T>() != null
				select obj;
			return a;
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
		
		// Update is called once per frame
		void Update ()
		{
			objectCount = _players.Count;
		}
	}
}