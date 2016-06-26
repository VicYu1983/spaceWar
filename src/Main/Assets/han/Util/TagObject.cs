using UnityEngine;
using System.Collections;
using Han.Util;

namespace Han.Util
{
	public class TagObject : MonoBehaviour, ITagObject, ITagManagerListener
	{
		public int seqId;
		public string tagName;

		public int SeqID{ get { return seqId; } set{ seqId = value; } }
		public string Tag{ get { return tagName; } set { tagName = value; } }
		public GameObject Belong{ get{ return this.gameObject; } }

		ITagManager tagmgr;
		public ITagManager TagManager{set{ tagmgr = value; }}
		public void OnManage(ITagObject obj){}
		public void OnUnManage(ITagObject obj){}

		void Start ()
		{
			EventManager.Singleton.Add(this);
			tagmgr.Manage (this);
		}

		void OnDestroy(){
			tagmgr.Unmanage (this);
			EventManager.Singleton.Remove(this);
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}