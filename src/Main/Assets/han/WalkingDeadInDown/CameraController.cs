using System;
using UnityEngine;
using Han.Util;

namespace WalkingDeadInDown.Model
{
	public class CameraController : MonoBehaviour, ITagManagerListener
	{
		public GameObject maincamera;
		public float factor = 0.5f;
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

				var v = pos - maincamera.transform.position;
				var curr = maincamera.transform.position;
				curr += v * factor;
				curr.z = z;
				maincamera.transform.position = curr;
			}
		}

		public ITagManager TagManager{ get; set; }

		public void OnManage(ITagObject obj){
			if (obj.Tag == "player") {
				player = obj.Belong;
			}
		}
		public void OnUnManage(ITagObject obj){
			if (obj.Tag == "player") {
				player = null;
			}
		}
	}
}

