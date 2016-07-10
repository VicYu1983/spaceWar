using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Han.Util;

public class ViewGamePlay : MonoBehaviour, ITagManagerListener {

	public GameObject player;
	public GameObject enemy;

	Hashtable modelObjs = new Hashtable();
	Hashtable viewObjs = new Hashtable();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		EventManager.Singleton.Add (this);
	}

	void OnDestroy(){
		EventManager.Singleton.Remove (this);
	}

	ITagManager tagManager;
	public ITagManager TagManager{ set{ tagManager = value; } }

	public void OnManage(ITagObject obj){
		print (obj.Tag);

		switch (obj.Tag) {
		case "player":
			break;
		case "enemy":
			break;
		default:
			break;
		}

		modelObjs.Add (obj.SeqID, obj.Belong);
	}

	public void OnUnManage(ITagObject obj){
		print (obj.Tag);

		modelObjs.Remove (obj.SeqID);
	}
}
