using UnityEngine;
using System.Collections;
using View;

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
		
		public void ChangePage ( PageName pageName ){
			print ("ChangePage" + pageName.ToString ());

			GameObject newPage = null;
			if (currentPage != null) {
				if (currentPage.name != pageName.ToString()) {
					//close current page and new page

					print (currentPage.name + "Close");
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
				currentPage.name = pageName.ToString();
			}
		}

		public void OpenPopup( PageName pageName ){

		}

		public void ClosePopup( PageName pageName ){

		}

		GameObject pageFactory( PageName pageName ){
			switch (pageName) {
			case PageName.IntroPage:
				return Instantiate (startPrefab);
			case PageName.GameplayPage:
				return Instantiate (gameplayPrefab);
			default:
				return null;
			}
		}
	}

}