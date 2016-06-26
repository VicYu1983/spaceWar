using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWar.Model
{
	public delegate float FuzzyValue();

	public class Fuzzy : MonoBehaviour
	{
		List<FuzzyValue> values = new List<FuzzyValue>();
		public List<float> vs = new List<float> ();

		public int AddFuzzyValue(FuzzyValue value){
			values.Add (value);
			vs.Add (0);
			return values.Count-1;
		}

		public int Best(){
			var max = 0.0;
			var maxi = 0;
			for (int i = 0; i < values.Count; ++i) {
				var v = values [i] ();
				if (v > max) {
					max = v;
					maxi = i;
				}
				vs [i] = v;
			}
			return maxi;
		}

		GameObject target;
		public GameObject Target {
			get{ 
				return target;
			}
			set{ 
				target = value;
			}
		}
	}
}

