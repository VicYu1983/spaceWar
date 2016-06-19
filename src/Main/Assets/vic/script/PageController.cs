using System;
using UnityEngine;
using View;
using Model;

namespace View
{
	public class PageController: MonoBehaviour, IBasicPageListener, IGameListener
	{
		public PageController ()
		{
			
		}

		public void OnGameStateChange(GameState old, GameState newstate){
			print ("OnGameStateChange: " + newstate);
			switch (newstate) {
			case GameState.Win:
				GameContext.single.PageManager.OpenPopup (PageName.EndPanel);
				break;
			}
		}

		public void OnClick( PageName pageName, string btnName ){
			print (pageName + ":" + btnName);
			switch (pageName) {
			case PageName.IntroPage:
				switch (btnName) {
				case "btn_start":
					GameContext.single.PageManager.ChangePage ( PageName.GameplayPage );
					break;
				}
				break;
			case PageName.EndPanel:
				switch (btnName) {
				case "btn_again":
					GameContext.single.PageManager.PlayAnimation (PageName.GameplayPage.ToString () + "Open");
					GameContext.single.PageManager.ClosePopup (PageName.EndPanel);
					break;
				case "btn_exit":
					GameContext.single.PageManager.ChangePage ( PageName.IntroPage );
					GameContext.single.PageManager.ClosePopup (PageName.EndPanel);
					break;
				}
				break;
			}
		}

		public void OnAnimationTrigger( PageName pageName, string eventName ){

		}
		
		void Start () {
			GameContext.single.EventManager.Add (this);
			GameContext.single.PageManager.ChangePage ( PageName.IntroPage );
		}

		void OnDestroy(){
			GameContext.single.EventManager.Remove (this);
		}
	}
}

