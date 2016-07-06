using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class SigmoidLayer : ILayer
	{
		public int out_depth, out_sx, out_sy;
		public Vol in_act, out_act;

		public SigmoidLayer(int x, int y, int depth){
			out_depth = depth;
			out_sx = x;
			out_sy = y;
		}

		public Vol Forward (Vol V, bool is_training){
			this.in_act = V;
			var V2 = V.CloneAndZero();
			var N = V.w.Length;
			var V2w = V2.w;
			var Vw = V.w;
			for(var i=0;i<N;i++) { 
				V2w[i] = 1.0f/(1.0f+(float)Math.Exp(-Vw[i]));
			}
			this.out_act = V2;
			return this.out_act;
		}

		public float Backward(object y){
			var V = this.in_act; // we need to set dw of this
			var V2 = this.out_act;
			var N = V.w.Length;
			V.dw = Util.Zeros(N); // zero out gradient wrt data
			for(var i=0;i<N;i++) {
				var v2wi = V2.w[i];
				V.dw[i] =  v2wi * (1.0f - v2wi) * V2.dw[i];
			}
			return 0;
		}

		public void GetParamsAndGrads (List<ParamsAndGrads> list){

		}
	}
}

