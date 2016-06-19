using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model;

public class GamePlayUI : MonoBehaviour, ITagManagerListener {

	public GameObject bloodbarPrefab;
	public Vector3 offset;

	List<GameObject> bloodbars = new List<GameObject>();
	List<GameObject> gameobjects = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		GameContext.single.EventManager.Add (this);
	}

	void OnDestroy(){
		GameContext.single.EventManager.Remove (this);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < bloodbars.Count; ++i) {
			float hp = bloodbars[i].GetComponent<Player> ().HP;
			float maxhp = 100;
			float per = hp / maxhp;

			gameobjects [i].transform.localPosition = bloodbars [i].GetComponent<Player> ().body.transform.position + offset;
			gameobjects [i].transform.FindChild ("blood").transform.localScale = new Vector3 (per, 1, 1);
		}
	}

	public void OnManage(ITagObject obj){
		
		if (obj.Tag == "enemy" || obj.Tag == "player") {
			Player p = obj.Belong.GetComponent<Player> ();
			bloodbars.Add (obj.Belong);

			GameObject bloodbar = Instantiate (bloodbarPrefab, p.body.transform.position, new Quaternion ()) as GameObject;
			bloodbar.transform.parent = this.transform;
			Vector3 np = bloodbar.transform.localPosition;
			np.z = 0;
			bloodbar.transform.localPosition = np;
			gameobjects.Add ( bloodbar );
		}
	}

	public void OnUnManage(ITagObject obj){
		//Debug.Log (obj.Tag + ":" + obj.SeqID+":destroy");

		if (obj.Tag == "enemy" || obj.Tag == "player") {

			int removeId = bloodbars.IndexOf (obj.Belong);
			bloodbars.Remove (obj.Belong);

			Destroy (gameobjects [removeId]);
			gameobjects.RemoveAt (removeId);
		}
	}
}
