using System;
using UnityEngine;

namespace Model
{
	public class ObjectFactory : MonoBehaviour, IObjectFactory
	{
		public GameObject player, bullet;
		public GameObject CreateObject( ObjectType type, Vector3 location, Quaternion rotation, object info ){
			switch (type) {
			case ObjectType.Player:
				return Instantiate (player, location, rotation) as GameObject;
			case ObjectType.Bullet:
				return Instantiate (bullet, location, rotation) as GameObject;
			default:
				return Instantiate (player, location, rotation) as GameObject;
			}
		}
	}
}

