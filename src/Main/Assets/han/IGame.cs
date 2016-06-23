using System;
using UnityEngine;

namespace Model
{
	public enum GameState{
		Pending,
		Win,
		Lose,
		Play
	}

	public interface IGame
	{
		int Level{ get; }
		Vector2 Size{ get; }
		GameState State{ get; }
		void StartGame(int level);
		void DestroyGame();
	}
}

