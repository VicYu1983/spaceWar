using System;
using Han.Util;
using System.Collections.Generic;
using UnityEngine;

namespace WalkingDeadInDown.Model
{
	public class InputManager : ITagManagerListener
	{
		List<GameObject> cantouchs = new List<GameObject>();

		public ITagManager TagManager{ set; get; }

		public void OnManage(ITagObject obj){
			if (obj.Belong.GetComponent<CanTouch> () != null) {
				cantouchs.Add (obj.Belong);
			}
		}

		public void OnUnManage(ITagObject obj){
			if (obj.Belong.GetComponent<CanTouch> () != null) {
				cantouchs.Remove (obj.Belong);
			}
		}
	}
}

