using System;
using UnityEngine;

namespace SpaceWar.Model
{
	public class ObjectFactory : MonoBehaviour, IObjectFactory
	{
		public GameObject player, enemy, bullet, explode, explode2, explode3, itemHeal;
		public GameObject CreateObject( ObjectType type, Vector3 location, Quaternion rotation, object info ){
			GameObject ret;
			switch (type) {
			case ObjectType.ItemHeal:
				ret = Instantiate (itemHeal, location, rotation) as GameObject;
				break;
			case ObjectType.Player:
				ret = Instantiate (player, location, rotation) as GameObject;
				break;
			case ObjectType.Enemy:
				ret = Instantiate (enemy, location, rotation) as GameObject;
				break;
			case ObjectType.Bullet:
				ret = Instantiate (bullet, location, rotation) as GameObject;
				break;
			case ObjectType.Explode:
				ret = Instantiate (explode, location, rotation) as GameObject;
				break;
			case ObjectType.Explode2:
				ret = Instantiate (explode2, location, rotation) as GameObject;
				break;
			case ObjectType.Explode3:
				ret = Instantiate (explode3, location, rotation) as GameObject;
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

