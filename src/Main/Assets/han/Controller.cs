using UnityEngine;
using System.Collections;
using UniRx;

namespace Model
{
	public class Controller : MonoBehaviour
	{
		IGameContext ctx;

		void Start ()
		{
			ctx = GameContext.single;
			ctx.ObjectFactory.CreateTest ();
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}