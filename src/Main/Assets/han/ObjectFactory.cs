using System;
using UnityEngine;

namespace Model
{
	public class ObjectFactory : MonoBehaviour, IObjectFactory
	{
		public GameObject player;
		public GameObject CreateObject( ObjectType type, Vector3 location = new Vector3(), Quaternion rotation = new Quaternion() ){
			switch (type) {
			case ObjectType.Player:
				return Instantiate (player, location, new Quaternion ()) as GameObject;
			default:
				return Instantiate (player, location, new Quaternion ()) as GameObject;
			}

		}
	}
}

