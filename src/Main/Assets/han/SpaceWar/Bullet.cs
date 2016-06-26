using System;
using UnityEngine;

namespace SpaceWar.Model
{
	public class Bullet : MonoBehaviour
	{
		public GameObject body;
		public int Power {
			get {
				return 10;
			}
		}
	}
}

