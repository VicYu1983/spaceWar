using UnityEngine;
using System.Collections;
using UniRx;

namespace Model
{
	public class GameContext : MonoBehaviour, IGameContext
	{
		public static GameContext single;

		public MonoBehaviour pageManager = null;
		public IPageManager PageManager { get{ return pageManager as IPageManager; } }

		public MonoBehaviour playerManager = null;
		public IPlayerManager PlayerManager { get{ return playerManager as IPlayerManager; } }

		EventManager eventManager = new EventManager();
		public IEventManager EventManager { get{ return eventManager; } }

		public MonoBehaviour objectFactory;
		public IObjectFactory ObjectFactory{ get{ return objectFactory as IObjectFactory; } }

		void Awake(){
			single = this;
		}
	}
}