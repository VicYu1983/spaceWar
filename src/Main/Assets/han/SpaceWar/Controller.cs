using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;
using SpaceWar.View;
using Han.Util;
using Vic.Util;

namespace SpaceWar.Model
{
	public class Controller : MonoBehaviour, IKeyListener, ITagManagerListener, IBasicPageListener
	{
		void Start (){
			EventManager.Singleton.Add(this);
			StartCoroutine (AppStart ());
		}
		void OnDestroy(){
			EventManager.Singleton.Remove(this);
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
			
		}
		ITagManager tagmgr;
		public ITagManager TagManager{set{ tagmgr = value; }}

		public void OnManage(ITagObject obj){
			
		}
		public void OnUnManage(ITagObject obj){
			
		}
		public void OnClick( PageName pageName, string btnName ){
			if (pageName == PageName.EndPanel) {
				if (btnName == "btn_again") {
					GameContext.single.Game.StartGame (0);
				} else if (btnName == "btn_exit") {
					GameContext.single.Game.DestroyGame ();
				}
			}
		}
		public void OnAnimationTrigger( PageName pageName, string eventName ){
			if (pageName == PageName.GameplayPage) {
				if (eventName == "GameStart") {
					GameContext.single.Game.StartGame (0);
				}
			}
		}

	}
}