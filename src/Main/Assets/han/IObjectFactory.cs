using System;
using UnityEngine;

namespace Model
{
	public interface IObjectFactory{
		GameObject CreateTest();
		GameObject CreateObject( String name, Vector3 location );
	}


}

