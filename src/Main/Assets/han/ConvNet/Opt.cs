using System;

namespace Han.ConvNet
{
	public struct Opt
	{
		public int sx,sy,depth;
		public int num_neurons;
		public float bias_pref;
		public float l1_decay_mul, l2_decay_mul;
	}
}

