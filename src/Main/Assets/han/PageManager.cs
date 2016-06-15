using UnityEngine;
using System.Collections;

namespace Model
{
	public class PageManager : MonoBehaviour, IPageManager
	{

		public GameObject startPrefab;
		public GameObject gameplayPrefab;

		GameObject currentPage;
		GameObject[] popups;

		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
		
		public void ChangePage ( string pageName ){
			GameObject newPage = null;
			if (currentPage != null) {
				if (currentPage.name != pageName) {
					//close current page and new page
					currentPage.GetComponent<Animator>().Play( currentPage.name + "Close" );
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
			}
		}

		public void OpenPopup( string pageName ){

		}

		public void ClosePopup( string pageName ){

		}

		GameObject pageFactory( string pageName ){
			switch (pageName) {
			case "startPage":
				return Instantiate (startPrefab);
			case "gameplayPage":
				return Instantiate (gameplayPrefab);
			default:
				return null;
			}
		}
	}

}