using UnityEngine;
using System.Collections;

namespace Model
{
	public class PageManager : MonoBehaviour, IPageManager
	{

		public GameObject startPrefab;
		public GameObject gameplayPrefab;

		// Use this for initialization
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
		
		public void ChangePage ( string name ){

		}

		public void OpenPopup( string name ){

		}

		public void ClosePopup( string name ){

		}
	}

}