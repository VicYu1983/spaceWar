using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class ReluLayer
	{
		int out_depth, out_sx, out_sy;
		Vol in_act, out_act;

		public ReluLayer (int x, int y, int depth)
		{
			out_depth = depth;
			out_sx = x;
			out_sy = y;
		}
		public Vol Forward (Vol V, bool is_training){
			this.in_act = V;
			var V2 = V.Clone();
			var N = V.w.Length;
			var V2w = V2.w;
			for(var i=0;i<N;i++) { 
				if(V2w[i] < 0) V2w[i] = 0; // threshold at 0
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
				if(V2.w[i] <= 0) V.dw[i] = 0; // threshold
				else V.dw[i] = V2.dw[i];
			}
			return 0;
		}

		public void GetParamsAndGrads (List<ParamsAndGrads> list){

		}
	}
}

