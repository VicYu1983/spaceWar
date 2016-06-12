using System;
using UnityEngine;

namespace Model
{
	public interface IObjectFactory{
		GameObject CreateObject( ObjectType type, Vector3 location, Quaternion rotation );
	}
}

