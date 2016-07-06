using System;
using UnityEngine;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class TestNet : MonoBehaviour
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

			net.AddLayer (new FullyConnLayer (2, 2));
			net.AddLayer (new SigmoidLayer (1, 1, 2));

			net.AddLayer (new FullyConnLayer (2, 1));
			net.AddLayer (new SigmoidLayer (1, 1, 1));

			net.AddLayer (new RegressionLayer (1, 1, 1));
			/*
			Vol action;

			for (var i = 0; i < 5000; ++i) {
				trainer.Train (new Vol(new float[]{ 0, 0 }), 0f);
				trainer.Train (new Vol(new float[]{ 1, 1 }), 0f);
				trainer.Train (new Vol(new float[]{ 1, 0 }), 1f);
				trainer.Train (new Vol(new float[]{ 0, 1 }), 1f);

				if (i % 100 == 0) {
					action = net.Forward (new Vol (new float[]{ 0, 0 }));
					print ("after:"+action.w[0]);
					action = net.Forward (new Vol (new float[]{ 1, 1 }));
					print ("after:"+action.w[0]);
					action = net.Forward (new Vol (new float[]{ 1, 0 }));
					print ("after:"+action.w[0]);
					action = net.Forward (new Vol (new float[]{ 0, 1 }));
					print ("after:"+action.w[0]);
					print ("========" + i + "=========");
				}
			}
			*/
		}

		void Update(){

			for (var i = 0; i < iteration; ++i) {
				foreach (Vector3 v in data) {
					trainer.Train (new Vol(new float[]{ v.x, v.y }), v.z);
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
					Vol action = net.Forward (new Vol (new float[]{ i/10f, j/10f }));
					float output = action.w[0];
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

