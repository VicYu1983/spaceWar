using UnityEngine;
using System.Collections;
using UniRx;
using Han.Util;

namespace SpaceWar.Model
{
	public class GameContext : MonoBehaviour, IGameContext
	{
		public static GameContext single;

		public MonoBehaviour pageManager = null;
		public IPageManager PageManager { get{ return pageManager as IPageManager; } }

		public MonoBehaviour tagManager = null;
		public ITagManager TagManager { get{ return tagManager as ITagManager; } }

		public MonoBehaviour objectFactory;
		public IObjectFactory ObjectFactory{ get{ return objectFactory as IObjectFactory; } }

		public MonoBehaviour game;
		public IGame Game{ get{ return game as IGame; } }

		void Awake(){
			single = this;
		}
	}
}