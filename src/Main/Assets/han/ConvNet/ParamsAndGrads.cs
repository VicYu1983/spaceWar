using System;

namespace Han.ConvNet
{
	public struct ParamsAndGrads
	{
		public float[] param, grads;
		public float l1_decay_mul, l2_decay_mul;
	}
}

