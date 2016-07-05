using System;

namespace Han.ConvNet
{
	public class Util
	{
		static System.Random rand = new System.Random();

		public static float[] Zeros(int n){
			return new float[n];
		}

		public static float Randn(float mu, float std){
			return mu + (float)rand.NextDouble () * std;
		}

		public static float Tanh(float x) {
			var y = (float)Math.Exp(2 * x);
			return (y - 1) / (y + 1);
		}
	}
}

