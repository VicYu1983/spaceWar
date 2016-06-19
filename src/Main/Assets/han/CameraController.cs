using System;
using UnityEngine;

namespace Model
{
	public class CameraController : MonoBehaviour, ITagManagerListener
	{
		public GameObject maincamera;
		public GameObject player;

		void Start (){
			GameContext.single.EventManager.Add(this);
		}
		void OnDestroy(){
			GameContext.single.EventManager.Remove(this);
		}
		void Update(){
			if (maincamera!=null && player != null) {
				var z = maincamera.transform.position.z;
				var pos = player.transform.position;
				pos.z = z;
				maincamera.transform.position = pos;
			}
		}

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

