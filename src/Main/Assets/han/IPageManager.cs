using UnityEngine;
using System.Collections;

namespace Model
{
	public interface IPageManager
	{
		void ChangePage (string pageName );
		void OpenPopup( string pageName );
		void ClosePopup( string pageName );
	}
}