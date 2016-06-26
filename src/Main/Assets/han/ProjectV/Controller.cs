using System;
using UnityEngine;
using Han.Util;

namespace ProjectV.Model
{
	public class Controller : MonoBehaviour, IModelListener
	{
		void Awake(){
			EventManager.Singleton.Add (this);
		}
		void Start(){
			model.StartGame (0);
		}
		void OnDestroy(){
			model.DestroyGame ();
			EventManager.Singleton.Remove (this);
		}
		IModel model;
		public IModel Model {
			set{ model = value; }
		}
		public void OnGameStart(){
			print ("OnGameStart");
		}
	}
}

