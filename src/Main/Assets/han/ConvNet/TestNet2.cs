using System;
using UnityEngine;
using System.Collections.Generic;

namespace ConvNetSharp
{
	public class TestNet2 : MonoBehaviour
	{
		public int iteration = 10;
		public List<Vector3> data;
		public GameObject pixel;

		Net net;
		Trainer trainer;
		GameObject[,] pixels = new GameObject[10, 10];

		void Start(){
			for (var i = 0; i < pixels.GetLength(0); ++i) {
				for (var j = 0; j < pixels.GetLength(1); ++j) {
					pixels [i, j] = Instantiate (pixel, new Vector3 (i, j, 0), new Quaternion()) as GameObject;
				}
			}

			net = new Net();
			trainer = new Trainer(net);

			net.AddLayer (new InputLayer (1, 1, 2));

			net.AddLayer (new FullyConnLayer (2, Activation.Sigmoid));

			net.AddLayer (new FullyConnLayer (1, Activation.Sigmoid));

			net.AddLayer (new RegressionLayer (1));
		}

		void Update(){

			for (var i = 0; i < iteration; ++i) {
				foreach (Vector3 v in data) {
					trainer.Train (new Volume(new double[]{ v.x, v.y }), v.z);
				}
			}

			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				var pos = ray.origin;
				pos /= 10.0f;
				pos.z = 0;
				data.Add (pos);
			}

			if (Input.GetMouseButtonDown (1)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				var pos = ray.origin;
				pos /= 10.0f;
				pos.z = 1;
				data.Add (pos);
			}

			for (var i = 0; i < pixels.GetLength(0); ++i) {
				for (var j = 0; j < pixels.GetLength(1); ++j) {
					Volume action = net.Forward (new Volume (new double[]{ i/10f, j/10f }));
					double output = action.Weights[0];
					if (output > 0.5) {
						pixels [i, j].GetComponent<MeshRenderer> ().material.color = new Color (1, 0, 0);
					} else {
						pixels [i, j].GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 1);
					}
				}
			}
		}
	}
}

