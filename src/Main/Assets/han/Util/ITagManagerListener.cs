using System;

namespace Han.Util
{
	public interface ITagManagerListener
	{
		ITagManager TagManager{ set; }
		void OnManage(ITagObject obj);
		void OnUnManage(ITagObject obj);
	}
}

