using System;

namespace Model
{
	public interface ITagManagerListener
	{
		void OnManage(ITagObject obj);
		void OnUnManage(ITagObject obj);
	}
}

