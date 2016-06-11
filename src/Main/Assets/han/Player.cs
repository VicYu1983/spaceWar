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

	void Start ()
	{
		GameContext.single.EventManager.Add(this);
		GameContext.single.PlayerManager.Manage (this);
	}

	void Destroy(){
		GameContext.single.PlayerManager.Unmanage (this);
		GameContext.single.EventManager.Remove(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

}