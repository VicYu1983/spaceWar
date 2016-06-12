using UnityEngine;
using System.Collections;
using UniRx;
using System.Linq;

namespace Model
{
	public class Controller : MonoBehaviour, IKeyListener
	{
		void Start (){
			GameContext.single.EventManager.Add(this);
			StartCoroutine (AppStart ());
		}
		IEnumerator AppStart(){
			yield return 0;
			GameContext.single.ObjectFactory.CreateObject (ObjectType.Player);
		}
		void Destroy(){
			GameContext.single.EventManager.Remove(this);
		}
		public void OnKeyDown(KeyCode code){
			var player = GameContext.single.TagManager.FindObjectsWithTag ("player").FirstOrDefault() as MonoBehaviour;
			if (player == null) {
				return;
			}
			var ctr = player.GetComponent<Player> ();
			switch (code) {
			case KeyCode.F:
				{
					ctr.InvokeShield ();
				}
				break;
			case KeyCode.Space:
				{
					ctr.Shoot ();
				}
				break;
			}
		}
		public void OnKeyHold(KeyCode code){
			var player = GameContext.single.TagManager.FindObjectsWithTag ("player").FirstOrDefault() as MonoBehaviour;
			if (player == null) {
				return;
			}
			var ctr = player.GetComponent<Player> ();
			switch (code) {
			case KeyCode.UpArrow:
				{
					ctr.Forward (10000);
				}
				break;
			case KeyCode.DownArrow:
				{
					ctr.Forward (-10000);
				}
				break;
			case KeyCode.LeftArrow:
				{
					ctr.Rotate (10000);
				}
				break;
			case KeyCode.RightArrow:
				{
					ctr.Rotate (-10000);
				}
				break;
			}
		}
		public void OnKeyUp(KeyCode code){
			Debug.Log ("OnKeyUp:"+code);


		}
	}
}