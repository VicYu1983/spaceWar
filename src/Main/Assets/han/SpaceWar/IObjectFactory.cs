using System;
using UnityEngine;

namespace SpaceWar.Model
{
	public interface IObjectFactory{
		GameObject CreateObject( ObjectType type, Vector3 location = new Vector3(), Quaternion rotation = new Quaternion(), object info = null );
	}
}

