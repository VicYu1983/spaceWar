using System;
using UnityEngine;

namespace Model
{
	public class ObjectFactory : MonoBehaviour, IObjectFactory
	{
		public GameObject player, bullet, explode;
		public GameObject CreateObject( ObjectType type, Vector3 location, Quaternion rotation, object info ){
			GameObject ret;
			switch (type) {
			case ObjectType.Player:
				ret = Instantiate (player, location, rotation) as GameObject;
				break;
			case ObjectType.Bullet:
				ret = Instantiate (bullet, location, rotation) as GameObject;
				break;
			default:
				ret = Instantiate (player, location, rotation) as GameObject;
				break;
			}
			if (ret != null) {
				ret.SetActive (true);
			}
			return ret;
		}
	}
}

