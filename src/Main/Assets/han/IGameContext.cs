using UnityEngine;
using System.Collections;
using UniRx;

namespace Model
{
	public interface IGameContext
	{
		IPageManager PageManager { get; }
		IPlayerManager PlayerManager { get; }
		IObjectFactory ObjectFactory{ get; }
		IEventManager EventManager { get; }
	}
}