using UnityEngine;
using System.Collections;
using UniRx;

public interface IGameContext
{
	IPageManager PageManager { get; }
	IPlayerManager PlayerManager { get; }
	IEventManager EventManager { get; }
	Subject<object[]> RxAction { get; }
	void PushAction (string cmd, object ps);
}