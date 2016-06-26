using System;
using UnityEngine;

namespace ProjectV.Model
{
	public class Controller : MonoBehaviour, IView
	{
		public IModel model = new Model();
		public IView view;

		public IModel Model{ get{ return model; } set{ } }

		void Start(){
			view = this;
			view.Model = model;

			model.StartGame (0);
		}
		void OnDestroy(){
			
		}
	}
}

