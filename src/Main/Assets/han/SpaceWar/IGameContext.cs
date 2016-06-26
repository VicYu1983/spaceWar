using UnityEngine;
using System.Collections;
using UniRx;
using Han.Util;

namespace SpaceWar.Model
{
	public interface IGameContext
	{
		IPageManager PageManager { get; }
		ITagManager TagManager { get; }
		IObjectFactory ObjectFactory{ get; }
		IGame Game{ get; }
	}
}