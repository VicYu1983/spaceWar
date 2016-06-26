using System;

namespace Vic.Util
{
	public interface IBasicPageListener
	{
		void OnClick( PageName pageName, string btnName );
		void OnAnimationTrigger( PageName pageName, string eventName );
	}
}

