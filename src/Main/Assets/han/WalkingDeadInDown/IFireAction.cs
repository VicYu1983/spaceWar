using System;

namespace WalkingDeadInDown.Model
{
	public interface IFireAction
	{
		FireSystem FireSystem{ get; set; }
		void Init();
		bool Update();
		void Cancel();
	}
}

