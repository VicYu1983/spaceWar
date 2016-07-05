using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class RegressionLayer : ILayer
	{
		public int out_depth, out_sx, out_sy;
		public Vol in_act, out_act;

		public RegressionLayer (Opt opt)
		{
			out_depth = opt.depth;
			out_sx = opt.sx;
			out_sy = opt.sy;
		}

		public Vol Forward (Vol V, bool is_training){
			this.in_act = V;
			this.out_act = V;
			return this.out_act;
		}

		public float Backward(object y){
			var x = this.in_act;
			x.dw = Util.Zeros(x.w.Length);
			var loss = 0.0f;
			if(y is float[]) {
				for(var i=0;i<this.out_depth;i++) {
					var dy = x.w[i] - (y as float[])[i];
					x.dw[i] = dy;
					loss += 0.5f*dy*dy;
				}
			} else if( y is float ) {
				var dy = x.w[0] - (float)y;
				x.dw[0] = dy;
				loss += 0.5f*dy*dy;
			} else if( y is Dictionary<string,float>){
				var yo = y as Dictionary<string,float>;
				var i = yo["dim"];
				var yi = yo["val"];
				var dy = x.w[(int)i] - yi;
				x.dw[(int)i] = dy;
				loss += 0.5f*dy*dy;
			}
			return loss;
		}

		public void GetParamsAndGrads (List<ParamsAndGrads> list){
		
		}
	}
}

