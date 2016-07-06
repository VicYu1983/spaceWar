using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class FullyConnLayer : ILayer
	{
		public int out_depth, out_sx, out_sy;
		public int num_inputs;
		public Vol[] filters;
		public Vol biases;
		public Vol in_act, out_act;
		public float l1_decay_mul, l2_decay_mul;

		public FullyConnLayer (int depth, int numOfOutput, float bias_pref = 0.0f){
			this.out_depth = numOfOutput;
			this.num_inputs = 1 * 1 * depth;
			this.out_sx = 1;
			this.out_sy = 1;
			this.l1_decay_mul = 0.0f;
			this.l2_decay_mul = 1.0f;

			var bias = bias_pref;
			this.filters = new Vol[this.out_depth];
			for(var i=0;i<this.out_depth ;i++) {
				filters [i] = new Vol (1, 1, this.num_inputs);
			}
			this.biases = new Vol(1, 1, this.out_depth, bias);
		}

		public Vol Forward(Vol V, bool is_training) {
			this.in_act = V;
			var A = new Vol(1, 1, this.out_depth, 0.0f);
			var Vw = V.w;
			for(var i=0;i<this.out_depth;i++) {
				var a = 0.0f;
				var wi = this.filters[i].w;
				for(var d=0;d<this.num_inputs;d++) {
					a += Vw[d] * wi[d];
				}
				a += this.biases.w[i];
				A.w[i] = a;
			}
			this.out_act = A;
			return this.out_act;
		}

		public float Backward(object y){
			var V = this.in_act;
			V.dw = Util.Zeros(V.w.Length);

			for(var i=0;i<this.out_depth;i++) {
				var tfi = this.filters[i];
				var chain_grad = this.out_act.dw[i];
				for(var d=0;d<this.num_inputs;d++) {
					V.dw[d] += tfi.w[d]*chain_grad; // grad wrt input data
					tfi.dw[d] += V.w[d]*chain_grad; // grad wrt params
				}
				this.biases.dw[i] += chain_grad;
			}
			return 0;
		}

		public void GetParamsAndGrads(List<ParamsAndGrads> list) {
			ParamsAndGrads obj;
			for(var i=0;i<this.out_depth;i++) {
				obj.param = this.filters [i].w;
				obj.grads = this.filters [i].dw;
				obj.l1_decay_mul = this.l1_decay_mul;
				obj.l2_decay_mul = this.l2_decay_mul;
				list.Add (obj);
			}
			obj.param = this.biases.w;
			obj.grads = this.biases.dw;
			obj.l1_decay_mul = 0.0f;
			obj.l2_decay_mul = 0.0f;
			list.Add (obj);
		}
	}
}

