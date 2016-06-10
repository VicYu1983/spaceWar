using UnityEngine;
using System.Collections;
using UniRx;

public class Controller : MonoBehaviour
{
	IGameContext ctx;

	void Start ()
	{
		ctx = GameContext.single;
		ctx.RxAction.Subscribe(
			action => {
				string cmd = action[0] as string;
				object p = action[1] as object;
				Debug.Log(cmd);
				Debug.Log(p);
			},
			e => {},
			() => {});

		ctx.PushAction ("create player", 1);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

