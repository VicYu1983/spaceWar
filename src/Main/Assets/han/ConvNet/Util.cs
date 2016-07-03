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
	}
}

