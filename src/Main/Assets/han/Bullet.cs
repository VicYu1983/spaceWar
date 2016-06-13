using System;
using UnityEngine;

namespace Model
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

