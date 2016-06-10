using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IEventSender{
	void OnAddReceiver(object receiver);
	void OnRemoveReceiver(object receiver);
}

public interface IEventManager{
	void AddSender(IEventSender sender);
	void AddReceiver(object receiver);
	void RemoveSender(IEventSender sender);
	void RemoveReceiver(object receiver);
	void Add (object obj);
	void Remove (object obj);
}


public class EventManager : IEventManager{
	HashSet<IEventSender> _senders = new HashSet<IEventSender>();
	HashSet<object> _receivers = new HashSet<object>();

	public static IEventManager Singleton = new EventManager();

	public void AddSender(IEventSender sender){
		_senders.Add(sender);
		_receivers.ToList().ForEach(receiver=>sender.OnAddReceiver(receiver));
	}

	public void AddReceiver(object receiver){
		_receivers.Add(receiver);
		_senders.ToList().ForEach(sender=>sender.OnAddReceiver(receiver));
	}

	public void RemoveSender(IEventSender sender){
		_receivers.ToList().ForEach(receiver=>sender.OnRemoveReceiver(receiver));
		_senders.Remove(sender);
	}

	public void RemoveReceiver(object receiver){
		_senders.ToList().ForEach(sender=>sender.OnRemoveReceiver(receiver));
		_receivers.Remove(receiver);
	}

	public void Add(object obj){
		if (obj is IEventSender) {
			AddSender (obj as IEventSender);
		}
		AddReceiver (obj);
	}

	public void Remove(object obj){
		if (obj is IEventSender) {
			RemoveSender (obj as IEventSender);
		}
		RemoveReceiver (obj);
	}
}


public interface IEventSenderVerifyProxyDelegate : IEventSender{
	bool VerifyReceiverDelegate(object receiver);
}

public class EventSenderVerifyProxy : IEventSender{
	IEventSenderVerifyProxyDelegate _submgr;
	HashSet<object> _receivers = new HashSet<object>();
	public EventSenderVerifyProxy(IEventSenderVerifyProxyDelegate submgr){
		_submgr = submgr;
	}
	public IEnumerable<object> Receivers{ get{ return _receivers; } }
	public void OnAddReceiver(object receiver){
		if (_submgr.VerifyReceiverDelegate (receiver)) {
			_receivers.Add (receiver);
			_submgr.OnAddReceiver (receiver);
		}
	}
	public void OnRemoveReceiver(object receiver){
		if (_submgr.VerifyReceiverDelegate (receiver)) {
			_receivers.Remove (receiver);
			_submgr.OnRemoveReceiver (receiver);
		}
	}
}