using System;
using UnityEngine;
using System.Collections.Generic;

namespace Model
{
	public class PlayerSystem : MonoBehaviour, ITagManagerListener, IKeyListener
	{
		List<ITagObject> players = new List<ITagObject>();

		void Start (){
			GameContext.single.EventManager.Add(this);
		}
		void OnDestroy(){
			GameContext.single.EventManager.Remove(this);
		}

		public void OnManage(ITagObject obj){
			if (obj.Tag == "player" && obj.Belong.GetComponent<Player> () != null) {
				players.Add (obj);
			}
		}

		public void OnUnManage(ITagObject obj){
			if (obj.Tag == "player" && obj.Belong.GetComponent<Player> () != null) {
				players.Remove (obj);
			}
		}

		public void OnKeyDown(KeyCode code){
			foreach (var obj in players) {
				var ctr = obj.Belong.GetComponent<Player> ();
				switch (code) {
				case KeyCode.F:
					{
						ctr.InvokeShield ();
					}
					break;
				case KeyCode.Space:
					{
						ctr.Shoot ();
					}
					break;
				}
			}
		}
		public void OnKeyHold(KeyCode code){
			foreach (var obj in players) {
				var ctr = obj.Belong.GetComponent<Player> ();
				switch (code) {
				case KeyCode.UpArrow:
					{
						ctr.Forward (10000);
					}
					break;
				case KeyCode.DownArrow:
					{
						ctr.Forward (-10000);
					}
					break;
				case KeyCode.LeftArrow:
					{
						ctr.Rotate (2000);
					}
					break;
				case KeyCode.RightArrow:
					{
						ctr.Rotate (-2000);
					}
					break;
				case KeyCode.L:
					{
						ctr.MoveTo (new Vector3 (0, 0, 0), 10000, 2000);
					}
					break;
				}
			}

		}
		public void OnKeyUp(KeyCode code){

		}
	}
}

