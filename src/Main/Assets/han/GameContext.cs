using UnityEngine;
using System.Collections;
using UniRx;

public class GameContext : MonoBehaviour, IGameContext
{
	public static GameContext single;

	public MonoBehaviour pageManager = null;
	public IPageManager PageManager { get{ return pageManager as IPageManager; } }

	public MonoBehaviour playerManager = null;
	public IPlayerManager PlayerManager { get{ return playerManager as IPlayerManager; } }

	EventManager eventManager = new EventManager();
	public IEventManager EventManager { get{ return eventManager; } }

	Subject<object[]> action = new Subject<object[]>();
	public Subject<object[]> RxAction { get { return action; } }

	void Awake(){
		single = this;
	}

	public void PushAction(string cmd, object ps){
		action.OnNext(new object[]{cmd, ps});
	}
}