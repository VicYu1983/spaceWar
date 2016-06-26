using System;
using UnityEngine;
using Han.Util;

namespace SpaceWar.Model
{
	public class CameraController : MonoBehaviour, ITagManagerListener
	{
		public GameObject maincamera;
		public GameObject player;

		void Start (){
			EventManager.Singleton.Add (this);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove(this);
		}
		void Update(){
			if (maincamera!=null && player != null) {
				var z = maincamera.transform.position.z;
				var pos = player.transform.position;
				pos.z = z;
				maincamera.transform.position = pos;
			}
		}
		ITagManager tagmgr;
		public ITagManager TagManager{set{ tagmgr = value; }}

		public void OnManage(ITagObject obj){
			if (obj.Tag == "player") {
				player = obj.Belong.GetComponent<Player>().body;
			}
		}
		public void OnUnManage(ITagObject obj){
			if (obj.Tag == "player") {
				player = null;
			}
		}
	}
}

