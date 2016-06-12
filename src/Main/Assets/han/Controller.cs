using UnityEngine;
using System.Collections;
using UniRx;

namespace Model
{
	public class Controller : MonoBehaviour, IKeyListener
	{
		void Start ()
		{
			GameContext.single.EventManager.Add(this);
			GameObject ship = GameContext.single.ObjectFactory.CreateObject (ObjectType.Player, new Vector3(), new Quaternion());
			ship.AddComponent<TagObject> ();
			//ship.GetComponent<Player> ().Group = "player";
		}

		void Destroy(){
			GameContext.single.EventManager.Remove(this);
		}

		public void OnKeyDown(KeyCode code){
			Debug.Log ("OnKeyDown:"+code);
		}
		public void OnKeyHold(KeyCode code){
			Debug.Log ("OnKeyHold:"+code);
		}
		public void OnKeyUp(KeyCode code){
			Debug.Log ("OnKeyUp:"+code);
		}
	}
}