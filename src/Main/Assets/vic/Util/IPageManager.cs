using UnityEngine;
using System.Collections;

namespace Vic.Util
{
	public interface IPageManager
	{
		void ChangePage ( string pageName );
		void OpenPopup( string pageName );
		void ClosePopup( string pageName );
		void PlayAnimation (string animationName);
	}
}