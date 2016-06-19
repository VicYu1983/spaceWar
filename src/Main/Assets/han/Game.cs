using System;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace Model
{
	public class Game : MonoBehaviour, IGame, ICollideSenderListener, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy proxy;
		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
		}
		void Start (){
			GameContext.single.EventManager.Add(this);
			GameContext.single.EventManager.Add (proxy);
		}
		void OnDestroy(){
			DestroyGame ();
			GameContext.single.EventManager.Remove(proxy);
			GameContext.single.EventManager.Remove(this);
		}
		void Update(){
			if (State != GameState.Play) {
				return;
			}
			var enemies = GameContext.single.TagManager.FindObjectsWithTag ("enemy");
			if (enemies.Count() == 0) {
				State = GameState.Win;
			}
		}
		public void OnAddReceiver(object receiver){

		}
		public void OnRemoveReceiver(object receiver){

		}
		public bool VerifyReceiverDelegate(object receiver){
			return receiver is IGameListener;
		}
		public int level=0;
		public GameState state = GameState.Pending;
		public int Level{ get{ return level; } }
		public GameState State{ 
			get { return state; } 
			set{
				if (state != value) {
					print ("XXXXXXXXX:" + state + ":" + value);
					foreach (var obj in proxy.Receivers) {
						(obj as IGameListener).OnGameStateChange (state, value);
					}
					state = value;
					print ("FFFFFFF:" + state + ":" + value);
				} else {
					print ("SSSS");
				}
			}
		}

		public void StartGame(int level){
			DestroyGame ();
			GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);

			var enemies = 
				from idx in Enumerable.Range(0, 2) 
				select GameContext.single.ObjectFactory.CreateObject (ObjectType.Enemy);
			foreach (var enemy in enemies) {
				enemy.GetComponent<TagObject> ().Tag = "enemy";
			}
			State = GameState.Play;
		}
		public void DestroyGame(){
			var players = 
				from obj in GameContext.single.TagManager.FindObjectsWithTag ("player")
				select obj.Belong;
			foreach (var p in players) {
				Destroy (p);
			}
			var enemys = 
				from obj in GameContext.single.TagManager.FindObjectsWithTag ("enemy")
				select obj.Belong;
			foreach (var e in enemys) {
				Destroy (e);
			}
			State = GameState.Pending;
		}
		public void OnCollideEnter(Collision2D coll) {
			if (State != GameState.Play) {
				return;
			}
			if (coll.contacts.Length > 0) {
				var contact = coll.contacts [0];
				var obj1 = coll.contacts [0].collider.GetComponent<CollideSender> ().Belong;
				var obj2 = coll.contacts [0].otherCollider.GetComponent<CollideSender> ().Belong;

				if (coll.contacts [0].collider.gameObject.name == "shield") {
					GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode2, new Vector3 (contact.point.x, contact.point.y));
					return;
				}

				if (obj1.GetComponent<TagObject> ().Tag == "enemy") {
					var enemy = obj1.GetComponent<Player> ();
					if (obj2.GetComponent<TagObject> ().Tag == "bullet") {
						GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode3, new Vector3 (contact.point.x, contact.point.y));

						var bullet = obj2.GetComponent<Bullet> ();
						enemy.Damage (bullet.Power);

						Destroy (bullet.gameObject);
						if ( enemy.State == PlayerState.Destroy) {
							GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode, new Vector3 (contact.point.x, contact.point.y));
							Destroy (enemy.gameObject);
						}
					}
				}
			}
		}
	}
}

