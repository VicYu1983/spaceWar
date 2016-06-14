using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model;

public class GamePlayUI : MonoBehaviour, ITagManagerListener {

	public GameObject bloodbarPrefab;
	public Vector3 offset;

	List<GameObject> bs = new List<GameObject>();
	List<GameObject> vs = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		GameContext.single.EventManager.Add (this);
	}

	void OnDestroy(){
		GameContext.single.EventManager.Remove (this);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < bs.Count; ++i) {
			float hp = bs[i].GetComponent<Player> ().HP;
			float maxhp = 100;
			float per = hp / maxhp;

			vs [i].transform.position = bs [i].GetComponent<Player> ().body.transform.position + offset;
			vs [i].transform.FindChild ("blood").transform.localScale = new Vector3 (per, 1, 1);
		}
	}

	public void OnManage(ITagObject obj){
		//Debug.Log (obj.Tag + ":" + obj.SeqID+":create");

		if (obj.Tag == "enemy" || obj.Tag == "player") {
			Player p = obj.Belong.GetComponent<Player> ();
			bs.Add (obj.Belong);

			vs.Add (Instantiate (bloodbarPrefab, p.body.transform.position, new Quaternion()) as GameObject);
		}
	}

	public void OnUnManage(ITagObject obj){
		//Debug.Log (obj.Tag + ":" + obj.SeqID+":destroy");

		if (obj.Tag == "enemy" || obj.Tag == "player") {

			int removeId = bs.IndexOf (obj.Belong);
			bs.Remove (obj.Belong);

			Destroy (vs [removeId]);
			vs.RemoveAt (removeId);
		}
	}
}
