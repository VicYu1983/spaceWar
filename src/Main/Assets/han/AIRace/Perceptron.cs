using System;

namespace AIRace.Model
{
	public delegate float ActivateFn(float v);

	public class Perceptron
	{
		float[] ws;
		ActivateFn Activate;

		float[] inputs;
		float output;
		float err;

		public Perceptron (ActivateFn fn, int cnt=2){
			ws = new float[cnt + 1];
			Activate = fn;
			inputs = new float[cnt];
		}

		public float Error{ get{ return err; } }
		public float[] W{ get{ return ws; } }
		public float Output{ get{ return output; } }
		public float[] Input{ get{ return inputs; } 
			set{ 
				for (var i = 0; i < inputs.Length; ++i) {
					inputs [i] = value [i];
				}
			}
		}
		public void Feed(){
			float value = 0.0f;
			for (var i = 0; i < inputs.Length; ++i) {
				value += inputs [i] * ws [i];
			}
			value += 1 * ws [ws.Length - 1];
			output = Activate (value);
		}

		public void Learn(float target, float learningRate=0.7f){
			err = (target - Output);
			for(var i=0; i<inputs.Length; ++i){
				ws [i] += learningRate* inputs [i] * err;
			}
			ws[ws.Length-1] += learningRate* 1 * err;
		}
	}
}

