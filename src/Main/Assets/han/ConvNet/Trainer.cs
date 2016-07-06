using System;
using System.Collections.Generic;
using UnityEngine;

namespace Han.ConvNet
{
	public class Trainer
	{
		ILayer net;
		float momentum, learning_rate = 0.01f;
		float l1_decay = 0.001f, l2_decay = 0.001f;

		public Trainer (ILayer net){
			this.net = net;
		}

		public void Train(Vol x, object y){
			net.Forward (x, true);
			var cost_loss = net.Backward(y);
			//Debug.Log ("loss:" + cost_loss);

			var l2_decay_loss = 0.0f;
			var l1_decay_loss = 0.0f;

			List<ParamsAndGrads> pglist = new List<ParamsAndGrads> ();
			net.GetParamsAndGrads(pglist);

			for(var i=0;i<pglist.Count;i++) {
				var pg = pglist[i];
				var p = pg.param;
				var g = pg.grads;

				var l2_decay_mul = pg.l2_decay_mul;
				var l1_decay_mul = pg.l1_decay_mul;
				var l2_decay = this.l2_decay * l2_decay_mul;
				var l1_decay = this.l1_decay * l1_decay_mul;

				var plen = p.Length;
				for (var j = 0; j < plen; j++) {
					l2_decay_loss += l2_decay*p[j]*p[j]/2;
					l1_decay_loss += l1_decay*Math.Abs(p[j]);
					var l1grad = l1_decay * (p[j] > 0 ? 1 : -1);
					var l2grad = l2_decay * (p[j]);
					var gij = (l2grad + l1grad + g [j]);
					//p[j] +=  - this.learning_rate * gij;
					p[j] +=  - this.learning_rate * g[j];

					// 記得要歸0
					g[j] = 0.0f;
				}
			}
		}
	}
}

