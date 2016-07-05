using System;

namespace Han.ConvNet
{
	public class Vol
	{
		public int sx, sy, depth;
		public float[] w, dw;

		public Vol(float[] sx){
			this.sx = 1;
			this.sy = 1;
			this.depth = sx.Length;
			this.w = Util.Zeros(this.depth);
			this.dw = Util.Zeros(this.depth);
			for(var i=0;i<this.depth;i++) {
				this.w[i] = sx[i];
			}
		}

		public Vol(int sx, int sy, int depth){
			this.sx = sx;
			this.sy = sy;
			this.depth = depth;
			var n = sx*sy*depth;
			this.w = Util.Zeros(n);
			this.dw = Util.Zeros(n);
			float scale = (float)Math.Sqrt(1.0/(sx*sy*depth));
			for(var i=0;i<n;i++) { 
				this.w[i] = Util.Randn(-1f, 2f);
			}
		}

		public Vol (int sx, int sy, int depth, float c){
			this.sx = sx;
			this.sy = sy;
			this.depth = depth;
			var n = sx*sy*depth;
			this.w = Util.Zeros(n);
			this.dw = Util.Zeros(n);
			for(var i=0;i<n;i++) { 
				this.w[i] = c;
			}
		}

		public float GetW(int x, int y, int d) { 
			var ix=((this.sx * y)+x)*this.depth+d;
			return this.w[ix];
		}

		public void SetW(int x, int y, int d, float v){ 
			var ix=((this.sx * y)+x)*this.depth+d;
			this.w[ix] = v; 
		}

		public void AddW(int x, int y, int d, float v) { 
			var ix=((this.sx * y)+x)*this.depth+d;
			this.w[ix] += v; 
		}

		public Vol CloneAndZero(){
			return new Vol (this.sx, this.sy, this.depth, 0.0f);
		}
	}
}

