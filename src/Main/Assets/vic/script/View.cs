using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class View : MonoBehaviour {
	List<GameObject> gs = new List<GameObject>();

	public GameObject pre_player;

	// Use this for initialization
	void Start () {
		CreatePlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreatePlayer(){
		GameObject p = Instantiate (pre_player);
		p.transform.parent = this.transform;
		p.name = "player";
		gs.Add (p);
	}
}
