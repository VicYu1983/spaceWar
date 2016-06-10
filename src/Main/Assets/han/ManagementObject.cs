using UnityEngine;
using System.Collections;

public class ManagementObject : MonoBehaviour, IPlayer, IPlayerManagerListener
{
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

	void Destory(){
		GameContext.single.EventManager.Remove(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

