using System;
using UnityEngine;
using View;
using System.Linq;
using Han.Util;

namespace SpaceWar.Model
{
	public class ItemController : MonoBehaviour, IGameListener, IBasicPageListener
	{
		void Start (){
			EventManager.Singleton.Add(this);
		}
		void OnDestroy(){
			EventManager.Singleton.Remove(this);
		}

		public void OnClick( PageName pageName, string btnName ){
			if (pageName == PageName.EndPanel) {
				if (btnName == "btn_again") {
					var items = 
						from obj in GameContext.single.TagManager.FindObjectsWithComponent<Item> ()
						select obj.Belong;
					foreach (var item in items) {
						Destroy (item);
					}
				} else if (btnName == "btn_exit") {
					var items = 
						from obj in GameContext.single.TagManager.FindObjectsWithComponent<Item> ()
						select obj.Belong;
					foreach (var item in items) {
						Destroy (item);
					}
				}
			}
		}

		public void OnAnimationTrigger( PageName pageName, string eventName ){
			if (pageName == PageName.GameplayPage) {
				if (eventName == "GameStart") {
					
				}
			}
		}

		public void OnGameStateChange(GameState old, GameState newstate){
			if (newstate == GameState.Play) {
				GameContext.single.ObjectFactory.CreateObject (ObjectType.ItemHeal, new Vector2 (15, 15));
				GameContext.single.ObjectFactory.CreateObject (ObjectType.ItemHeal, new Vector2 (15, -15));
				GameContext.single.ObjectFactory.CreateObject (ObjectType.ItemHeal, new Vector2 (-15, -15));
				GameContext.single.ObjectFactory.CreateObject (ObjectType.ItemHeal, new Vector2 (-15, 15));
			}
		}

		public void OnManage(ITagObject obj){
			if (obj.Belong.GetComponent<Item> () != null) {

			}
		}

		public void OnUnManage(ITagObject obj){
			if (obj.Belong.GetComponent<Item> () != null) {
				
			}
		}
	}
}

