using System;
using UnityEngine;

namespace Han.ConvNet
{
	public class TestNet : MonoBehaviour
	{
		Net net;
		Trainer trainer;

		void Start(){
			net = new Net();
			trainer = new Trainer(net);

			Opt opt = new Opt();
			opt.sx = 1;
			opt.sy = 1;
			opt.depth = 1;
			net.AddLayer (new InputLayer (opt));

			opt.depth = 1;
			opt.num_neurons = 1;
			net.AddLayer (new FullyConnLayer (opt));

			net.AddLayer (new TanhLayer (opt));

			opt.sx = 1;
			opt.sy = 1;
			opt.depth = 1;
			net.AddLayer (new RegressionLayer (opt));

			Vol action;
			action = net.Forward (new Vol (new float[]{ 1 }));
			print ("before:"+action.w[0]);


			for (var i = 0; i < 1000; ++i) {
				trainer.Train (new Vol(new float[]{ 1 }), 1f);

				if (i % 100 == 0) {
					action = net.Forward (new Vol (new float[]{ 1 }));
					print ("after:"+action.w[0]);
				}
			}



		}
	}
}

