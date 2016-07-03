using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public class Net : ILayer
	{
		List<ILayer> layers = new List<ILayer>();

		public Vol Forward(Vol V, bool is_training = false) {
			var act = this.layers[0].Forward(V, is_training);
			for(var i=1;i<this.layers.Count;i++) {
				act = this.layers[i].Forward(act, is_training);
			}
			return act;
		}

		public float Backward(object y) {
			var N = this.layers.Count;
			var loss = this.layers[N-1].Backward(y);
			for(var i=N-2;i>=0;i--) {
				this.layers[i].Backward(null);
			}
			return loss;
		}

		public void GetParamsAndGrads(List<ParamsAndGrads> list) {
			for(var i=0;i<this.layers.Count;i++) {
				this.layers [i].GetParamsAndGrads (list);
			}
		}
	}
}

