using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;

namespace Model
{
	public class Controller : MonoBehaviour, IKeyListener, ICollideSenderListener, ITagManagerListener
	{
		void Start (){
			GameContext.single.EventManager.Add(this);
			StartCoroutine (AppStart ());
		}
		IEnumerator AppStart(){
			yield return 0;
			GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);

			var enemies = 
				from idx in Enumerable.Range(0, 2) 
				select GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);
			foreach (var enemy in enemies) {
				enemy.GetComponent<TagObject> ().Tag = "enemy";
			}
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
					ctr.Rotate (2000);
				}
				break;
			case KeyCode.RightArrow:
				{
					ctr.Rotate (-2000);
				}
				break;
			}
		}
		public void OnKeyUp(KeyCode code){
			
		}

		public void OnCollideEnter(Collision2D coll) {
			if (coll.contacts.Length > 0) {
				var contact = coll.contacts [0];
				var obj1 = coll.contacts [0].collider.GetComponent<CollideSender> ().Belong;
				var obj2 = coll.contacts [0].otherCollider.GetComponent<CollideSender> ().Belong;

				//Debug.Log (obj1.GetComponent<TagObject> ().Tag);
				//Debug.Log (obj2.GetComponent<TagObject> ().Tag);

				if (coll.contacts [0].collider.gameObject.name == "shield") {
					GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode2, new Vector3 (contact.point.x, contact.point.y));
					return;
				}

				if (obj1.GetComponent<TagObject> ().Tag == "enemy") {
					var player = obj1.GetComponent<Player> ();
					if (obj2.GetComponent<TagObject> ().Tag == "bullet") {
						GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode3, new Vector3 (contact.point.x, contact.point.y));

						var bullet = obj2.GetComponent<Bullet> ();
						player.Damage (bullet.Power);

						Destroy (bullet.gameObject);
						if ( player.State == PlayerState.Destroy) {
							GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode, new Vector3 (contact.point.x, contact.point.y));
							Destroy (player.gameObject);
						}
					}
				}
			}
		}

		public void OnManage(ITagObject obj){
			
		}
		public void OnUnManage(ITagObject obj){
			
		}
	}
}