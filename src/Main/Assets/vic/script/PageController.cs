using System;
using UnityEngine;
using View;
using SpaceWar.Model;
using Han.Util;

namespace SpaceWar.View
{
	public class PageController: MonoBehaviour, IBasicPageListener, IGameListener
	{
		public PageController ()
		{
			
		}

		public void OnGameStateChange(GameState old, GameState newstate){
			//print ("OnGameStateChange: " + newstate);
			switch (newstate) {
			case GameState.Win:
				GameContext.single.PageManager.OpenPopup (PageName.EndPanel);
				break;
			case GameState.Lose:
				GameContext.single.PageManager.OpenPopup (PageName.EndPanel);
				break;
			}
		}

		public void OnClick( PageName pageName, string btnName ){
			//print (pageName + ":" + btnName);
			switch (pageName) {
			case PageName.IntroPage:
				switch (btnName) {
				case "btn_start":
					//Open Animation include game start event!!!
					GameContext.single.PageManager.ChangePage ( PageName.GameplayPage );
					break;
				}
				break;
			case PageName.EndPanel:
				switch (btnName) {
				case "btn_again":
					//Open Animation include game start event!!!
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
			EventManager.Singleton.Add (this);
			GameContext.single.PageManager.ChangePage ( PageName.IntroPage );
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}
	}
}

