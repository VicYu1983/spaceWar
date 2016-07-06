using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class InputLayer : ILayer
	{
		int out_depth, out_sx, out_sy;
		Vol in_act, out_act;

		public InputLayer (int x, int y, int depth)
		{
			out_depth = depth;
			out_sx = x;
			out_sy = y;
		}

		public Vol Forward(Vol V, bool is_training) {
			this.in_act = V;
			this.out_act = V;
			return this.out_act;
		}

		public float Backward(object y){ return 0; }

		public void GetParamsAndGrads(List<ParamsAndGrads> list) {}
	}
}

