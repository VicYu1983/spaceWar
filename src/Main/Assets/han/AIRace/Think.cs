using System;
using UnityEngine;

namespace AIRace.Model
{
	public class Think : MonoBehaviour
	{
		public GameObject teacher;
		public GameObject me;
		public bool learn = true;

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
			float[] state = TeacherCar.State;
			bpn.Input = state;
			bpn.Feed ();
			if (learn) {
				float[] action = TeacherCar.Action;
				//print (state[0]+","+state[1]+","+state[2]+":"+action [0] + "," + action [1]);
				bpn.Learn (action, 1);
			} else {
				float[] currAction = bpn.Output;
				MyCar.PerformAction (currAction, Time.deltaTime);
			}
		}
	}
}

