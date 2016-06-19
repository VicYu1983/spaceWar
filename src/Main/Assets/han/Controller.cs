using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;
using View;

namespace Model
{
	public class Controller : MonoBehaviour, IKeyListener, ITagManagerListener, IBasicPageListener
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
			
		}

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