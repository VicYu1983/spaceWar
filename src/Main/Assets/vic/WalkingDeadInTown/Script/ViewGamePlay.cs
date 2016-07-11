using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Han.Util;
using WalkingDeadInDown.Model;

namespace WalkingDeadInTown.View{
	public class ViewGamePlay : MonoBehaviour, ITagManagerListener, IFireSystemListener {

		public GameObject player;
		public GameObject enemy;
		public GameObject ground;

		Hashtable modelObjs = new Hashtable();
		Hashtable viewObjs = new Hashtable();
		
		// Update is called once per frame
		void Update () {
			foreach (DictionaryEntry entry in modelObjs) {
				GameObject viewobj = viewObjs [entry.Key] as GameObject;
				GameObject modelobj = entry.Value as GameObject;

				if (viewobj != null) {
					viewobj.GetComponent<Player> ().SetPosition (modelobj.transform.localPosition);
				}
			}
		}

		void Awake(){
			EventManager.Singleton.Add (this);
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}

		void Start(){
			GameObject g = Instantiate (ground);
			g.transform.parent = this.transform;
		}

		GameObject GetViewObjectBySeqId( int seqId ){
			return viewObjs [seqId] as GameObject;
		}

		public void OnFireSystemFire (FireSystem fs, GameObject target, object info){
			GameObject v = GetViewObjectBySeqId( fs.GetComponent<TagObject>().SeqID );
			if( v != null )
				v.GetComponent<Player> ().SetState ("Fire");
		}

		public void OnFireSystemSpecFire (FireSystem fs, GameObject target, object info){
			GameObject v = GetViewObjectBySeqId( fs.GetComponent<TagObject>().SeqID );
			if( v != null )
				v.GetComponent<Player> ().SetState ("SpecFire");
		}

		public void OnFireSystemStock (FireSystem fs, GameObject target, object info){
			GameObject v = GetViewObjectBySeqId( fs.GetComponent<TagObject>().SeqID );
			if( v != null )
				v.GetComponent<Player> ().SetState ("Stock");
		}

		public void OnFireSystemSwordAttack(FireSystem fs, GameObject target, object info){
			GameObject v = GetViewObjectBySeqId( fs.GetComponent<TagObject>().SeqID );
			if( v != null )
				v.GetComponent<Player> ().SetState ("SwordAttack");
		}

		ITagManager tagManager;
		public ITagManager TagManager{ set{ tagManager = value; } }

		public void OnManage(ITagObject obj){
			print (obj.Tag);

			GameObject instance;

			switch (obj.Tag) {
			case "player":
				instance = Instantiate (player);
				break;
			case "enemy":
				instance = Instantiate (player);
				break;
			default:
				instance = Instantiate (player);
				break;
			}

			instance.transform.parent = this.transform;

			modelObjs.Add (obj.SeqID, obj.Belong);
			viewObjs.Add (obj.SeqID, instance);
		}

		public void OnUnManage(ITagObject obj){
			print (obj.Tag);

			modelObjs.Remove (obj.SeqID);
			viewObjs.Remove (obj.SeqID);
		}
	}
}
