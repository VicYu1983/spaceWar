using UnityEngine;
using System.Collections;

namespace Model
{
	
public class Player : MonoBehaviour, IPlayer, IPlayerManagerListener
{
	public int key;
	public string group;

	public int Key{ get { return key; } set{ key = value; } }
	public string Group{ get { return group; } }

	IPlayerManager _playerManager;
	public void OnPlayerManager(IPlayerManager mgr){
		if (_playerManager == null) {
			_playerManager = mgr;
			_playerManager.Manage (this);
		}
	}

	void Start ()
	{
		GameContext.single.EventManager.Add(this);
	}

	void Destroy(){
		_playerManager.Unmanage (this);
		GameContext.single.EventManager.Remove(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

}