using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class InputLayer : ILayer
	{
		public int out_depth, out_sx, out_sy;
		public Vol in_act, out_act;

		public InputLayer (Opt opt)
		{
			out_depth = opt.depth;
			out_sx = opt.sx;
			out_sy = opt.sy;
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

