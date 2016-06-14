using UnityEngine;
using System.Collections;

namespace Model
{
	public interface IPageManager
	{
		void ChangePage (string name );
		void OpenPopup( string name );
		void ClosePopup( string name );
	}
}