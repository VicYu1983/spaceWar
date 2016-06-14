using UnityEngine;
using System.Collections;
using Model;

public class GamePlayUI : MonoBehaviour, ITagManagerListener {

	public GameObject bloodbarPrefab;

	// Use this for initialization
	void Start () {
		GameContext.single.EventManager.Add (this);
	}

	void OnDestroy(){
		GameContext.single.EventManager.Remove (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnManage(ITagObject obj){
		Debug.Log (obj.Tag + ":" + obj.SeqID+":create");
		if (obj.Tag == "enemy") {
			var enemy = GameContext.single.TagManager.FindObjectWithTagAndSeqID (obj.Tag, obj.SeqID);
			if (enemy != null) {
				var info = obj.Belong.GetComponent<Player> ();
				Debug.Log ("hp:"+info.HP);
			}
		}
	}

	public void OnUnManage(ITagObject obj){
		Debug.Log (obj.Tag + ":" + obj.SeqID+":destroy");
	}
}
