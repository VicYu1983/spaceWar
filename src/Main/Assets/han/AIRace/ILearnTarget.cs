using System;

namespace AIRace.Model
{
	public interface ILearnTarget
	{
		float[] State{ get; }
		float[] Action{ get; }
		void PerformAction(float[] action, float deltaTime);
	}
}

