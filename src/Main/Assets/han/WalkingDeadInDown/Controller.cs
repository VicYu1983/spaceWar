using System;
using UnityEngine;
using Han.Util;

namespace WalkingDeadInDown.Model
{
	public class Controller : MonoBehaviour, IFireSystemListener
	{

		public GameObject player, enemy;
		
		void Awake(){
			EventManager.Singleton.Add (this);
		}

		void Start(){
			var obj = Instantiate (player);
			obj.SetActive (true);
			/*
			var obj2 = Instantiate (enemy, new Vector3(2,2,0), new Quaternion()) as GameObject;
			obj2.SetActive (true);

			var obj3 = Instantiate (enemy, new Vector3(4,2,0), new Quaternion()) as GameObject;
			obj3.SetActive (true);
			*/
		}

		void OnDestroy(){
			EventManager.Singleton.Remove (this);
		}

		public void OnFireSystemFire (FireSystem fs, GameObject target, object info){
			print ("OnFireSystemFire");
		}
		public void OnFireSystemSpecFire (FireSystem fs, GameObject target, object info){
			print ("OnFireSystemSpecFire");
		}
		public void OnFireSystemStock (FireSystem fs, GameObject target, object info){
			print ("OnFireSystemStock");
		}
		public void OnFireSystemSwordAttack(FireSystem fs, GameObject target, object info){
			print ("OnFireSystemSwordAttack");
		}
	}
}

