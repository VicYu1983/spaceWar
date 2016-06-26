using System;

namespace ProjectV.Model
{
	public interface IModelListener
	{
		IModel Model{ set; }
		void OnGameStart();
	}
}

