using System;
using UnityEngine;

namespace Model
{
	public class ObjectFactory : MonoBehaviour, IObjectFactory
	{
		public GameObject test;
		public GameObject player;

		public GameObject CreateTest(){
			return Instantiate (test);
		}

		public GameObject CreateObject( String name, Vector3 location = new Vector3() ){
			switch (name) {
			case "player":
				return Instantiate (player, location, new Quaternion ()) as GameObject;
			default:
				return Instantiate (player, location, new Quaternion ()) as GameObject;
			}

		}
	}
}

