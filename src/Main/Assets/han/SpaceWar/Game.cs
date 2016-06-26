using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Han.Util;

namespace SpaceWar.Model
{
	public class Game : MonoBehaviour, IGame, ICollideSenderListener, IEventSenderVerifyProxyDelegate
	{
		EventSenderVerifyProxy proxy;
		System.Random random = new System.Random ();
		void Awake(){
			proxy = new EventSenderVerifyProxy (this);
		}
		void Start (){
			EventManager.Singleton.Add(this);
			EventManager.Singleton.Add (proxy);
		}
		void OnDestroy(){
			DestroyGame ();
			EventManager.Singleton.Remove(proxy);
			EventManager.Singleton.Remove(this);
		}
		void Update(){
			if (State == GameState.Play) {
				var player = GameContext.single.TagManager.FindObjectsWithTag ("player").FirstOrDefault ();
				if (player == null) {
					State = GameState.Lose;
					return;
				}

				var enemies = GameContext.single.TagManager.FindObjectsWithTag ("enemy");
				if (enemies.Count() == 0) {
					State = GameState.Win;
				}
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
		public Vector2 Size{ get{ return new Vector2 (2, 2); } }
		public GameState State{ 
			get { return state; } 
			set{
				if (state != value) {
					foreach (var obj in proxy.Receivers) {
						(obj as IGameListener).OnGameStateChange (state, value);
					}
					state = value;
				}
			}
		}

		public void StartGame(int level){
			StartCoroutine (StartGameStepByStep ());
		}
		IEnumerator StartGameStepByStep(){
			DestroyGame ();
			yield return 0;
			GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);
			var enemies = 
				from idx in Enumerable.Range(0, 2) 
				select GameContext.single.ObjectFactory.CreateObject (ObjectType.Enemy);
			foreach (var enemy in enemies) {
				enemy.GetComponent<Player> ().body.transform.localPosition = new Vector3 ((float)random.NextDouble()*10, (float)random.NextDouble()*10, 0);
				enemy.GetComponent<TagObject> ().Tag = "enemy";
			}
			yield return 0;
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
				if (coll.contacts [0].collider.GetComponent<CollideSender> () == null) {
					return;
				}
				if (coll.contacts [0].otherCollider.GetComponent<CollideSender> () == null) {
					return;
				}
				if (coll.contacts [0].collider.gameObject.name == "shield") {
					GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode2, new Vector3 (contact.point.x, contact.point.y));
					return;
				}
				var obj1 = coll.contacts [0].collider.GetComponent<CollideSender> ().Belong;
				var obj2 = coll.contacts [0].otherCollider.GetComponent<CollideSender> ().Belong;

				if (obj1.GetComponent<Player> () != null) {
					var p = obj1.GetComponent<Player> ();
					if (obj2.GetComponent<TagObject> ().Tag == "itemHeal") {
						Destroy (obj2);
						p.AddHP (100);
					}

					if (obj2.GetComponent<TagObject> ().Tag == "bullet") {
						GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode3, new Vector3 (contact.point.x, contact.point.y));

						var bullet = obj2.GetComponent<Bullet> ();
						p.Damage (bullet.Power);

						Destroy (bullet.gameObject);
						if ( p.State == PlayerState.Destroy) {
							GameContext.single.ObjectFactory.CreateObject (ObjectType.Explode, new Vector3 (contact.point.x, contact.point.y));
							Destroy (p.gameObject);
						}
					}
				}
			}
		}
	}
}

