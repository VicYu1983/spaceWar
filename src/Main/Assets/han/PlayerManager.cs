using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Model
{
public class PlayerManager : MonoBehaviour, IPlayerManager, IEventSenderVerifyProxyDelegate
{
	EventSenderVerifyProxy _sender;
	IGameContext _ctx;
	HashSet<IPlayer> _players = new HashSet<IPlayer>();
	int idx;

	public void OnAddReceiver(object receiver){
		(receiver as IPlayerManagerListener).OnPlayerManager (this);
	}
	public void OnRemoveReceiver(object receiver){
		
	}
	public bool VerifyReceiverDelegate(object receiver){
		return receiver is IPlayerManagerListener;
	}

	public void Manage(IPlayer player){
		player.Key = idx++;
		_players.Add (player);
		Debug.Log (player.Group+":"+player.Key);
	}

	public void Unmanage(IPlayer player){
		_players.Remove (player);
	}

	void Awake(){
		_sender = new EventSenderVerifyProxy (this);
	}

	void Start ()
	{
		_ctx = GameContext.single;
		_ctx.EventManager.Add (_sender);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

}