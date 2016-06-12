using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;

namespace Model
{
	public class Controller : MonoBehaviour, IKeyListener, ICollideSenderListener
	{
		void Start (){
			GameContext.single.EventManager.Add(this);
			StartCoroutine (AppStart ());
		}
		IEnumerator AppStart(){
			yield return 0;
			GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);
			var enemy = GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);
			enemy.GetComponent<TagObject> ().Tag = "enemy";
		}
		void OnDestroy(){
			GameContext.single.EventManager.Remove(this);
		}
		public void OnKeyDown(KeyCode code){
			var player = GameContext.single.TagManager.FindObjectsWithTag ("player").FirstOrDefault() as MonoBehaviour;
			if (player == null) {
				return;
			}
			var ctr = player.GetComponent<Player> ();
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
		public void OnKeyHold(KeyCode code){
			var player = GameContext.single.TagManager.FindObjectsWithTag ("player").FirstOrDefault() as MonoBehaviour;
			if (player == null) {
				return;
			}
			var ctr = player.GetComponent<Player> ();
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
					ctr.Rotate (4000);
				}
				break;
			case KeyCode.RightArrow:
				{
					ctr.Rotate (-4000);
				}
				break;
			}
		}
		public void OnKeyUp(KeyCode code){
			
		}

		public void OnCollideEnter(Collision2D coll) {
			if (coll.contacts.Length > 0) {
				var contact = coll.contacts [0];
				GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode, new Vector3 (contact.point.x, contact.point.y));
				Debug.Log ("controller collide player:" + coll.contacts [0].collider.GetComponent<TagObject> ().Tag);
				Debug.Log ("controller collide other:" + coll.contacts [0].otherCollider.GetComponent<TagObject> ().Tag);
			}
		}
	}
}