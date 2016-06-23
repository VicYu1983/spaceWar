using System;
using UnityEngine;

namespace Model
{
	public class Util
	{
		public static float NormalizeAngle(float angle){
			if (angle > Mathf.PI) {
				return  angle - Mathf.PI * 2;
			} else {
				return angle;
			}
		}
	}
}

