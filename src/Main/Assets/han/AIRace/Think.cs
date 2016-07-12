using System;
using UnityEngine;

namespace AIRace.Model
{
	public class Think : MonoBehaviour
	{
		public GameObject target;
		public bool learn = true;
		MultiLayerPerceptron bpn;

		ILearnTarget Target{ 
			get{
				return null;
			} 
		}

		void Start(){
			float[] state = Target.State;
			float[] action = Target.Action;

			bpn = new MultiLayerPerceptron (state.Length);
			var layer = new PerceptronLayer (state.Length);
			layer.Add (new Perceptron (state.Length));
			layer.Add (new Perceptron (state.Length));
			layer.Add (new Perceptron (state.Length));

			var layer2 = new PerceptronLayer (3);
			layer2.Add (new Perceptron (3));
			layer2.Add (new Perceptron (3));

			var layer3 = new PerceptronLayer (2);
			for (var i = 0; i < action.Length; ++i) {
				layer3.Add (new Perceptron (2));
			}

			bpn.Add (layer);
			bpn.Add (layer2);
			bpn.Add (layer3);
		}

		void Update(){
			float[] state = Target.State;
			bpn.Input = state;
			bpn.Feed ();
			if (learn) {
				float[] action = Target.Action;
				bpn.Learn (action);
			} else {
				float[] currAction = bpn.Output;
				Target.PerformAction (currAction, Time.deltaTime);
			}
		}
	}
}

