using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using View;

namespace Model
{
	public class PageManager : MonoBehaviour, IPageManager
	{

		public GameObject startPrefab;
		public GameObject gameplayPrefab;
		public GameObject endPanelPrefab;

		GameObject currentPage;
		List<GameObject> popups;

		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
		
		public void ChangePage ( PageName pageName ){
			print ("ChangePage" + pageName.ToString ());

			GameObject newPage = null;
			if (currentPage != null) {
				if (currentPage.name != pageName.ToString()) {
					//close current page and new page

					print (currentPage.name + "Close");
					try{
						currentPage.GetComponent<Animator>().Play( currentPage.name + "Close" );
					}catch( InvalidCastException e ){
						Destroy (currentPage.gameObject);
						print (e);
					}
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

		public void OpenPopup( PageName pageName ){
			GameObject popup = pageFactory (pageName);
			popups.Add (popup);
		}

		public void ClosePopup( PageName pageName ){
			GameObject popup = popups [popups.Count - 1];
			popups.Remove (popup);

			Destroy (popup);
		}

		GameObject pageFactory( PageName pageName ){
			switch (pageName) {
			case PageName.IntroPage:
				return Instantiate (startPrefab);
			case PageName.GameplayPage:
				return Instantiate (gameplayPrefab);
			case PageName.EndPanel:
				return Instantiate (endPanelPrefab);
			default:
				return null;
			}
		}
	}

}