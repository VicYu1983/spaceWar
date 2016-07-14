using System;
using UnityEngine;
using System.Collections.Generic;

namespace AIRace.Model
{
	public class Think : MonoBehaviour
	{
		public GameObject teacher;
		public GameObject me;
		public bool learn = true;
		public int historyCount;

		int addCount=0;
		List<float[]> statehistory = new List<float[]>();
		List<float[]> actionhistory = new List<float[]>();

		MultiLayerPerceptron bpn;

		ILearnTarget TeacherCar {
			get {
				return teacher.GetComponent<Car> ();
			}
		}

		ILearnTarget MyCar{ 
			get{
				return me.GetComponent<Car>();
			} 
		}

		void Start(){
			float[] state = TeacherCar.State;
			float[] action = TeacherCar.Action;

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

			/*
			for (var i = 0; i < 1000; ++i) {
				bpn.Input = new float[]{ 0, 0, 0 };
				bpn.Feed ();
				bpn.Learn (new float[]{ 1, 1 });

				bpn.Input = new float[]{ 1, 1, 1 };
				bpn.Feed ();
				bpn.Learn (new float[]{ 0, 0 });

				bpn.Input = new float[]{ 1, 0, 1 };
				bpn.Feed ();
				bpn.Learn (new float[]{ 0.5f, 0.5f });
			}

			bpn.Input = new float[]{ 0, 0, 0 };
			bpn.Feed ();
			var currAction = bpn.Output;
			print (currAction[0]+","+currAction[1]);

			bpn.Input = new float[]{ 1, 1, 1 };
			bpn.Feed ();
			currAction = bpn.Output;
			print (currAction[0]+","+currAction[1]);

			bpn.Input = new float[]{ 1, 0, 1 };
			bpn.Feed ();
			currAction = bpn.Output;
			print (currAction[0]+","+currAction[1]);
			*/
		}

		void Update(){
			if (addCount % 10 == 0) {
				float[] state2 = TeacherCar.State;
				float[] action = TeacherCar.Action;
				float[] s = new float[state2.Length];
				for (var i = 0; i < state2.Length; ++i) {
					s [i] = state2 [i];
				}
				float[] a = new float[action.Length];
				for (var i = 0; i < action.Length; ++i) {
					a [i] = action [i];
				}
				statehistory.Add (s);
				actionhistory.Add (a);

				if (statehistory.Count > 100) {
					statehistory.RemoveAt (0);
					actionhistory.RemoveAt (0);
				}

				historyCount = statehistory.Count;
			}
			++addCount;
		
			if (learn) {
				for (var i = 0; i < statehistory.Count; ++i) {
					var s = statehistory [i];
					var a = actionhistory [i];
					bpn.Input = s;
					bpn.Feed ();
					bpn.Learn (a);
				}
			}

			float[] state = MyCar.State;
			bpn.Input = state;
			bpn.Feed ();
			float[] currAction = bpn.Output;
			MyCar.PerformAction (currAction, Time.deltaTime);
		}
	}
}

