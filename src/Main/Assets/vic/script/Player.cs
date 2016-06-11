using System;
using UnityEngine;

namespace View
{
	public class Player : MonoBehaviour, Model.IKeyListener
	{
		public GameObject body;

		public Player ()
		{
			
		}

		void Start(){
			Model.GameContext.single.EventManager.Add (this);
		}

		void Destroy(){
			Model.GameContext.single.EventManager.Remove (this);
		}

		public void OnKeyDown(KeyCode code){
		
		}
		public void OnKeyHold(KeyCode code){
			switch (code) {
			case KeyCode.UpArrow:
				body.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (100, 0));
				print ("+++++++++++++++++++");
				break;
			}
		}
		public void OnKeyUp(KeyCode code){
			
		}
	}
}

