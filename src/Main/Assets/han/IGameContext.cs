using UnityEngine;
using System.Collections;
using UniRx;

namespace Model
{
	public interface IGameContext
	{
		IPageManager PageManager { get; }
		ITagManager TagManager { get; }
		IObjectFactory ObjectFactory{ get; }
		IEventManager EventManager { get; }
	}
}