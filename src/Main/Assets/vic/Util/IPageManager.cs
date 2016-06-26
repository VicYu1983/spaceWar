using UnityEngine;
using System.Collections;

namespace Vic.Util
{
	public interface IPageManager
	{
		void ChangePage ( PageName pageName );
		void OpenPopup( PageName pageName );
		void ClosePopup( PageName pageName );
		void PlayAnimation (string animationName);
	}
}