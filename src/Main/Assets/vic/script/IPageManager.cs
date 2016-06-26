﻿using UnityEngine;
using System.Collections;
using View;

namespace SpaceWar.Model
{
	public interface IPageManager
	{
		void ChangePage ( PageName pageName );
		void OpenPopup( PageName pageName );
		void ClosePopup( PageName pageName );
		void PlayAnimation (string animationName);
	}
}