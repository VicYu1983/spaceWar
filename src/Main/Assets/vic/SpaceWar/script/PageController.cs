using System;
using UnityEngine;
using SpaceWar.Model;
using Han.Util;
using Vic.Util;

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
				GameContext.single.PageManager.OpenPopup ("EndPanel");
				break;
			case GameState.Lose:
				GameContext.single.PageManager.OpenPopup ("EndPanel");
				break;
			}
		}

		public void OnClick( string pageName, string btnName ){
			//print (pageName + ":" + btnName);
			switch (pageName) {
			case "IntroPage":
				switch (btnName) {
				case "btn_start":
					//Open Animation include game start event!!!
					GameContext.single.PageManager.ChangePage ( "GameplayPage" );
					break;
				}
				break;
			case "EndPanel":
				switch (btnName) {
				case "btn_again":
					//Open Animation include game start event!!!
					GameContext.single.PageManager.PlayAnimation ("GameplayPageOpen");
					GameContext.single.PageManager.ClosePopup ("EndPanel");
					break;
				case "btn_exit":
					GameContext.single.PageManager.ChangePage ( "IntroPage" );
					GameContext.single.PageManager.ClosePopup ("EndPanel");
					break;
				}
				break;
			}
		}

		public void OnAnimationTrigger( string pageName, string eventName ){

		}
		
		void Start () {
			EventManager.Singleton.Add (this);
			GameContext.single.PageManager.ChangePage ( "IntroPage" );
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}
	}
}

