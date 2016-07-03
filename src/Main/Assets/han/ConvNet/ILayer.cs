using System;
using System.Collections.Generic;

namespace Han.ConvNet
{
	public interface ILayer
	{
		Vol Forward (Vol V, bool is_training);
		float Backward(object y);
		void GetParamsAndGrads (List<ParamsAndGrads> list);
	}
}

