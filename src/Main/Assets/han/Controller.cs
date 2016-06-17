using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;
using View;

namespace Model
{
	public class Controller : MonoBehaviour, IKeyListener, ICollideSenderListener, ITagManagerListener, IBasicPageListener
	{
		void Start (){
			GameContext.single.EventManager.Add(this);
			StartCoroutine (AppStart ());
		}
		void OnDestroy(){
			GameContext.single.EventManager.Remove(this);
		}
		IEnumerator AppStart(){
			yield return 0;
		}
		public void OnKeyDown(KeyCode code){
			
		}
		public void OnKeyHold(KeyCode code){
			
		}
		public void OnKeyUp(KeyCode code){
			
		}

		public void OnCollideEnter(Collision2D coll) {
			if (coll.contacts.Length > 0) {
				var contact = coll.contacts [0];
				var obj1 = coll.contacts [0].collider.GetComponent<CollideSender> ().Belong;
				var obj2 = coll.contacts [0].otherCollider.GetComponent<CollideSender> ().Belong;

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
		public void OnClick( PageName pageName, string btnName ){
			
		}
		public void OnAnimationTrigger( PageName pageName, string eventName ){
			if (pageName == PageName.GameplayPage) {
				if (eventName == "GameStart") {
					GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);

					var enemies = 
						from idx in Enumerable.Range(0, 2) 
						select GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);
					foreach (var enemy in enemies) {
						enemy.GetComponent<TagObject> ().Tag = "enemy";
					}
				}
			}
		}

	}
}