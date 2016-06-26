using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Vic.Util;

namespace Vic.Util
{
	public class PageManager : MonoBehaviour, IPageManager
	{

		public GameObject startPrefab;
		public GameObject gameplayPrefab;
		public GameObject endPanelPrefab;

		GameObject currentPage;
		List<GameObject> popups = new List<GameObject>();

		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
		
		void ClosePage( GameObject page ){
			print (page.name + "Close");
			try{
				page.GetComponent<Animator>().Play( page.name + "Close" );
			}catch( Exception e ){
				Destroy (page.gameObject);
				print (e);
			}
		}

		public void PlayAnimation( string animationName ){
			try{
				currentPage.GetComponent<Animator> ().Play (animationName);
			}catch( Exception e){
				print (e);
				// no animations
			}
		}
		
		public void ChangePage ( string pageName ){
			print ("ChangePage" + pageName.ToString ());

			GameObject newPage = null;
			if (currentPage != null) {
				if (currentPage.name != pageName.ToString()) {
					//close current page and new page

					ClosePage (currentPage);
					newPage = pageFactory( pageName );
				} else {
					//same page, do nothing!
				}
			} else {
				//new page
				newPage = pageFactory( pageName );
			}
			if (newPage != null) {
				currentPage = newPage;
				currentPage.transform.parent = this.transform;
				currentPage.name = pageName.ToString();
			}
		}

		public void OpenPopup( string pageName ){
			GameObject popup = pageFactory (pageName);
			popup.name = pageName.ToString();
			popup.transform.parent = this.transform;
			popups.Add (popup);
		}

		public void ClosePopup( string pageName ){
			if (popups.Count > 0) {
				GameObject popup = popups [popups.Count - 1];
				popups.Remove (popup);
				ClosePage (popup);
			}
		}

		GameObject pageFactory( string pageName ){
			switch (pageName) {
			case "IntroPage":
				return Instantiate (startPrefab);
			case "GameplayPage":
				return Instantiate (gameplayPrefab);
			case "EndPanel":
				return Instantiate (endPanelPrefab);
			default:
				return null;
			}
		}
	}

}