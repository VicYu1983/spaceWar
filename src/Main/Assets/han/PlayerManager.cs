using UnityEngine;
using System.Collections;
using UniRx;

public class PlayerManager : MonoBehaviour, IPlayerManager, IEventSenderVerifyProxyDelegate
{
	public GameObject model;
	EventSenderVerifyProxy sender;

	IGameContext ctx;

	public void OnAddReceiver(object receiver){
		(receiver as IPlayerManagerListener).OnPlayerManager (this);
	}
	public void OnRemoveReceiver(object receiver){
		
	}
	public bool VerifyReceiverDelegate(object receiver){
		return receiver is IPlayerManagerListener;
	}

	public void Manage(IPlayer player){}

	void Awake(){
		sender = new EventSenderVerifyProxy (this);
	}

	void Start ()
	{
		ctx = GameContext.single;
		ctx.EventManager.Add (sender);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

