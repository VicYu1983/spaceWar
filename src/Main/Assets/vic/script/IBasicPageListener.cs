using System;

namespace View
{
	public interface IBasicPageListener
	{
		void OnClick( string pageName, string btnName );
		void OnAnimationTrigger( string pageName, string eventName );
	}
}

