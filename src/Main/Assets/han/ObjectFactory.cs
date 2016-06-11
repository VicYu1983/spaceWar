using System;
using UnityEngine;

namespace Model
{
	public class ObjectFactory : MonoBehaviour, IObjectFactory
	{
		public GameObject test;

		public GameObject CreateTest(){
			return Instantiate (test);
		}
	}
}

